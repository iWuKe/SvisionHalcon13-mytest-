﻿/**************************************************************************************
**
**       Filename:  Client.cs
**
**    Description:  vision software create TCP Server , this file complete the Client 
**                  which is connected to tcp server      
**
**        Version:  1.0
**        Created:  2016-3-1
**       Revision:  none
**       Compiler:  vs2010
**
**         Author:  iGoahead (Software & Embedded Engineer), iGoahead@hotmail.com
**        Company:  SIASUN
**
****************************************************************************************/
using System;
using System.Net.Sockets;
using System.Text;
using Svision;

namespace TradeServer
{
    namespace Tcp
    {
        /// <summary>
        /// 缓冲区
        /// </summary>
        public class TcpBuffer
        {
            /// <summary>
            /// 缓冲区所在的字节数组
            /// </summary>
            private byte[] m_bytes;

            /// <summary>
            /// 缓冲区的起始位置
            /// </summary>
            private int    m_offset;

            /// <summary>
            /// 缓冲区的最大可用字节数
            /// </summary>
            private int    m_size;

            /// <summary>
            /// 缓冲区已使用的字节数
            /// </summary>
            private int    m_count;

            /// <summary>
            /// 创建全新的字节数组作为缓冲区，字节数组全部可用
            /// </summary>
            /// <param name="size">字节数组的大小</param>
            public TcpBuffer(int size)
            {
                m_size   = size;
                m_bytes  = new byte[size];
                m_offset = 0;
                m_count  = 0;
            }

            /// <summary>
            /// 使用已有的字节数组作为缓冲区，字节数组全部可用，且已填充满内容
            /// </summary>
            /// <param name="bytes">已有的字节数组</param>
            public TcpBuffer(byte[] bytes)
            {
                m_bytes = bytes;
                m_offset = 0;
                m_size = bytes.Length;
                m_count = m_size;
            }

            /// <summary>
            /// 缓冲区所在的内存块
            /// </summary>
            public byte[] bytes
            {
                get { return m_bytes; }
            }

            /// <summary>
            /// 缓冲区相对于内存块首字节的索引
            /// </summary>
            public int offset
            {
                get { return m_offset; }
            }

            /// <summary>
            /// 缓冲区的大小
            /// </summary>
            public int size
            {
                get { return m_size; }
            }

            /// <summary>
            /// 缓冲区里实际内容的字节数
            /// </summary>
            public int count
            {
                get { return m_count; }
                set { m_count = value > m_size ? m_size : value; }
            }

            public override string ToString()
            {
                return Encoding.Default.GetString(m_bytes, m_offset, m_count);
            }
        }



        /// <summary>
        /// 维护客户账号密码、客户端连接Socket、异步事件、通信缓冲区等信息，并负责与客户端通信
        /// </summary>
        class Client
        {
            /// <summary>
            /// 接收的客户端连接socket
            /// </summary>
            private Socket               m_socket;

            /// <summary>
            /// 异步操作事件，用于异步通信（Receive, Send操作）
            /// </summary>
            private SocketAsyncEventArgs m_readWriteEventArg;

            /// <summary>
            /// 与客户端通信的接收缓冲区
            /// </summary>
            private TcpBuffer            m_receiveBuffer;

            /// <summary>
            /// 与客户端通信的发送缓冲区
            /// </summary>
            private TcpBuffer            m_sendBuffer;


            /// <summary>
            /// 创建客户端，分配接收/发送缓冲区，创建异步接收事件
            /// </summary>
            /// <param name="socket">客户端连接socket</param>
            /// <param name="receiveBufferSize">接收缓冲区大小</param>
            /// <param name="sendBufferSize">发送缓冲区大小</param>
            /// 
            public Client(Socket socket, int receiveBufferSize, int sendBufferSize)
            {
                m_socket                      = socket;
                m_receiveBuffer               = new TcpBuffer(receiveBufferSize);
                m_sendBuffer                  = new TcpBuffer(sendBufferSize);
                m_readWriteEventArg           = new SocketAsyncEventArgs();
                m_readWriteEventArg.Completed += IO_Completed;
            }


            public void Start()
            {
                // 设置接收事件的缓冲区
                m_readWriteEventArg.SetBuffer(m_receiveBuffer.bytes,
                                              m_receiveBuffer.offset,
                                              m_receiveBuffer.size);

                // 异步接收客户端数据
                bool willRaiseEvent = m_socket.ReceiveAsync(m_readWriteEventArg);
                if (!willRaiseEvent)
                {
                    ProcessReceive();
                }
            }


            /// <summary>
            /// 异步等待客户端IO完成
            /// </summary>
            private void IO_Completed(object sender, SocketAsyncEventArgs readWriteEventArg)
            {
                switch (readWriteEventArg.LastOperation)
                {
                    case SocketAsyncOperation.Receive:
                        ProcessReceive();
                        break;
                    case SocketAsyncOperation.Send:
                        ProcessSend();
                        break;
                    default:
                        throw new ArgumentException("The last operation completed on the socket was not a receive or send");
                }
            }


            /// <summary>
            /// 处理接收到的客户端数据
            /// </summary>
            private void ProcessReceive()
            {
                if (m_readWriteEventArg.BytesTransferred > 0 &&
                    m_readWriteEventArg.SocketError == SocketError.Success)
                {
                    // 处理接收到的数据
                    m_receiveBuffer.count = m_readWriteEventArg.BytesTransferred;

                    UserCode.GetInstance().mainCodeRun();
                   
                    // 填充发送缓冲区
                    string sendStr = UserCode.GetInstance().SendBuf;
                    TcpBuffer sendPack  = new TcpBuffer(Encoding.Default.GetBytes(sendStr));
                    TcpBuffer sendBuffer = sendPack;//TcpProtocol.Pack(sendPack, 0);

                    System.Buffer.BlockCopy(sendBuffer.bytes, sendBuffer.offset, m_sendBuffer.bytes, m_sendBuffer.offset, sendBuffer.count);
                    m_sendBuffer.count = sendBuffer.count;

                    // 设置发送缓冲区
                    m_readWriteEventArg.SetBuffer(m_sendBuffer.bytes, m_sendBuffer.offset, m_sendBuffer.count);

                    // 异步发送

                    bool willRaiseEvent = m_socket.SendAsync(m_readWriteEventArg);
                    if (!willRaiseEvent)
                    {
                        ProcessSend();
                    }                   
                    
                }
                else
                {
                    Close();
                }
            }


            /// <summary>
            /// 完成向客户端发送数据任务
            /// </summary>
            private void ProcessSend()
            {
                if (m_readWriteEventArg.SocketError == SocketError.Success)
                {
                    // 开始下一次接收客户端数据
                    Start();
                }
                else
                {
                    Close();
                }
            }


            /// <summary>
            /// 关闭客户端连接
            /// </summary>
            private void Close()
            {
                Console.WriteLine("客户端退出" + m_socket);
                try
                {
                    m_socket.Shutdown(SocketShutdown.Send);
                }
                catch (Exception) { }
                m_socket.Close();
            }
        }
    }
}
