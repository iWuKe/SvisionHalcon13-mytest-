using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Svision
{
    /// <summary>  
    /// 客户端与服务器之间的会话类  
    /// </summary>  
    public class Session
    {
#region 字段
        /// <summary>  
        /// 接收数据缓冲区  
        /// </summary>  
        private byte[] _recvBuffer;

        /// <summary>  
        /// 客户端发送到服务器的报文  
        /// 注意:在有些情况下报文可能只是报文的片断而不完整  
        /// </summary>  
        private string _datagram;

        /// <summary>  
        /// 客户端的Socket  
        /// </summary>  
        private Socket _clientSock;

        public int recvLen;

#endregion

#region 属性

        /// <summary>  
        /// 接收数据缓冲区   
        /// </summary>  
        public byte[] RecvDataBuffer
        {
            get
            {
                return _recvBuffer;
            }
            set
            {
                _recvBuffer = value;
            }
        }

        /// <summary>  
        /// 存取会话的报文  
        /// </summary>  
        public string Datagram
        {
            get
            {
                return _datagram;
            }
            set
            {
                _datagram = value;
            }
        }

        /// <summary>  
        /// 获得与客户端会话关联的Socket对象  
        /// </summary>  
        public Socket ClientSocket
        {
            get
            {
                return _clientSock;

            }
        }


#endregion

        /// <summary>  
        /// 构造函数  
        /// </summary>  
        /// <param name="cliSock">会话使用的Socket连接</param>  
        public Session(Socket cliSock)
        {

            _clientSock = cliSock;
        }
        /// <summary>  
        /// 关闭会话  
        /// </summary>  
        public void Close()
        {

                //关闭数据的接受和发送  
                _clientSock.Shutdown(SocketShutdown.Both);
                System.Threading.Thread.Sleep(10);
                //清理资源  
                _clientSock.Close();


        }
    }

    public class AsyncEventArgs : EventArgs
    {
        public string _msg;                         // 提示信息  
        public Session _sessions;
        public bool IsHandled { get; set; }         // 是否已经处理过了
        public AsyncEventArgs(string msg)
        {
            this._msg = msg;
            IsHandled = false;
        }
        public AsyncEventArgs(Session session)
        {
            this._sessions = session;
            IsHandled = false;
        }
        public AsyncEventArgs(string msg, Session session)
        {
            this._msg = msg;
            this._sessions = session;
            IsHandled = false;
        }
    } 

    /// <summary>  
    /// 异步SOCKET 服务器  
    /// </summary>  
    public class AsyncServer : IDisposable
    {

#region Fields
 
        private int _maxClient;                             // 服务器程序允许的最大客户端连接数 
        private int _clientCount;                           // 当前的连接的客户端数 
        private Socket _serverSock;                         // 服务器使用的异步socket
        private List<Session> _clients;                     // 客户端会话列表
        private bool disposed = false;

#endregion


#region Properties

        public bool IsRunning { get; private set; }         // 服务器是否正在运行 
        public IPAddress Address { get; private set; }      // 监听的IP地址
        public int Port { get; private set; }               // 监听的端口
        public Encoding Encoding { get; set; }              // 通信使用的编码

#endregion

#region Ctors

        /// <summary>  
        /// 异步Socket TCP服务器  
        /// </summary>  
        /// <param name="listenPort">监听的端口</param>  
        public AsyncServer(int listenPort)
            : this(IPAddress.Any, listenPort, 1024)
        {
        }

        /// <summary>  
        /// 异步Socket TCP服务器  
        /// </summary>  
        /// <param name="localEP">监听的终结点</param>  
        public AsyncServer(IPEndPoint localEP)
            : this(localEP.Address, localEP.Port, 1024)
        {
        }

        /// <summary>  
        /// 异步Socket TCP服务器  
        /// </summary>  
        /// <param name="localIPAddress">监听的IP地址</param>  
        /// <param name="listenPort">监听的端口</param>  
        /// <param name="maxClient">最大客户端数量</param>  
        public AsyncServer(IPAddress localIPAddress, int listenPort, int maxClient)
        {
            this.Address = localIPAddress;
            this.Port = listenPort;
            this.Encoding = Encoding.Default;

            _maxClient = maxClient;
            _clients = new List<Session>();
            _serverSock = new Socket(localIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

#endregion


#region Server

        /// <summary>  
        /// 启动服务器  
        /// </summary>  
        /// <returns>异步TCP服务器</returns>  
        public AsyncServer Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                _serverSock.Bind(new IPEndPoint(this.Address, this.Port));
                _serverSock.Listen(1024);
                _serverSock.BeginAccept(new AsyncCallback(HandleAcceptConnected), _serverSock);
            }
            return this;
        }

        /// <summary>  
        /// 启动服务器  
        /// </summary>  
        /// <param name="backlog">  
        /// 服务器所允许的挂起连接序列的最大长度  
        /// </param>  
        /// <returns>异步TCP服务器</returns>  
        public AsyncServer Start(int backlog)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                _serverSock.Bind(new IPEndPoint(this.Address, this.Port));
                _serverSock.Listen(backlog);
                _serverSock.BeginAccept(new AsyncCallback(HandleAcceptConnected), _serverSock);
            }
            return this;
        }

        /// <summary>  
        /// 停止服务器  
        /// </summary>  
        /// <returns>异步TCP服务器</returns>  
        public AsyncServer Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                //RaiseClientDisconnected(_serverSock);
                _serverSock.Close();
                //TODO 关闭对所有客户端的连接  

            }
            return this;
        }

#endregion

#region Receive
        /// <summary>  
        /// 处理客户端连接  
        /// </summary>  
        /// <param name="ar"></param>  
        /// 
        public bool SocketConnected(Socket _socket)
        {
            try
            {
                return !_socket.Poll(1, SelectMode.SelectRead) && (_socket.Available == 0);
            }
            catch (SocketException ex)
            {
                return false;
            }
            catch (ObjectDisposedException)
            {
                return false;
            }
        }
        private void HandleAcceptConnected(IAsyncResult ar)
        {
            if (IsRunning)
            {
                //Socket server;
                // client;
                try
                {
                    Socket server = (Socket)ar.AsyncState;
                    Socket client = server.EndAccept(ar);
                    //检查是否达到最大的允许的客户端数目  
                    if (_clientCount == _maxClient)
                    {
                        //TODO 触发事件  
                        RaiseServerException(null);
                        //Session session = new Session(client);
                        //session.Close();
                        //return;
                       
                    }
                    else if (SocketConnected(client) == false)
                    {
                        RaiseServerException(null);
                    }
                    else
                    {
                        Session session = new Session(client);
                        try
                        {

                            if (SocketConnected(client))
                            {
                                lock (_clients)
                                {
                                    if (SocketConnected(client))
                                    {
                                        _clients.Add(session);
                                        if (SocketConnected(client))
                                        {
                                            _clientCount++;
                                            if (SocketConnected(client))
                                            {
                                                RaiseClientConnected(session); //触发客户端连接事件 
                                            }
                                            else
                                            {
                                                _clientCount--;
                                                session.Close();
                                            }
                                        }
                                        else
                                        {
                                            _clients.Remove(session);
                                            session.Close();
                                        }
                                    }

                                }
                                if (SocketConnected(client))
                                {
                                    session.RecvDataBuffer = new byte[client.ReceiveBufferSize];
                                    //开始接受来自该客户端的数据  
                                    if (SocketConnected(client))
                                    {
                                        
                                        client.BeginReceive(session.RecvDataBuffer, 0, session.RecvDataBuffer.Length, SocketFlags.None,
                                         new AsyncCallback(HandleDataReceived), session);
                                        Svision.GetMe().tcpStr="客户端已连接"+DateTime.Now.ToLongTimeString().ToString()+System.Environment.NewLine;
                                        Svision.GetMe().ShowTCPMessage();
                                    }
                                    else
                                    {
                                        session.Datagram = null;
                                        session.RecvDataBuffer = null;
                                        session.recvLen = 0;
                                        if (_clients.Count == 1)
                                        {
                                            _clients.Remove(session);

                                        }
                                        if (_clientCount == 1)
                                        {
                                            _clientCount--;
                                        }
                                        session.Close();
                                    }
                                }
                                else
                                {
                                    if (_clients.Count == 1)
                                    {
                                        _clients.Remove(session);
                                        if (_clientCount == 1)
                                        {
                                            _clientCount--;
                                        }
                                    }
                                    session.Close();
                                }
                            }
                        }
                        catch (SocketException ex)
                        {

                            session.Datagram = null;
                            session.RecvDataBuffer = null;
                            if (_clients.Count == 1)
                            {
                                _clients.Remove(session);

                            }
                            if (_clientCount == 1)
                            {
                                _clientCount--;
                            }
                            session.Close();
                        }




                    }

                    //接受下一个请求  
                    server.BeginAccept(new AsyncCallback(HandleAcceptConnected), ar.AsyncState);
                }
                catch (SocketException ex)
                {}
            }
        }
        /// <summary>  
        /// 处理客户端数据  
        /// </summary>  
        /// <param name="ar"></param>  
        private void HandleDataReceived(IAsyncResult ar)
        {
            if (IsRunning)
            {
 
                Session session = (Session)ar.AsyncState;
                Socket client = session.ClientSocket;
                try
                {
                    //如果两次开始了异步的接收,所以当客户端退出的时候  
                    //会两次执行EndReceive  
                    int recv = client.EndReceive(ar);
                    session.recvLen = recv;
/* */
                    if (recv == 0 || SocketConnected(session.ClientSocket) == false && _clients.Count == 1 && _clientCount == 1)
                    {
                        //TODO 触发事件 (关闭客户端)  
                        Svision.GetMe().tcpStr = "客户端已断开" + DateTime.Now.ToLongTimeString().ToString() + System.Environment.NewLine;
                        Svision.GetMe().ShowTCPMessage();
                        CloseSession(session);
                         RaiseNetError(session);
                        return;
                    }
                    /*  */
                    //TODO 处理已经读取的数据 ps:数据在session的RecvDataBuffer中  
    
                    RaiseDataReceived(session);
                    //TODO 触发数据接收事件  
                    if (SocketConnected(session.ClientSocket) == false && _clients.Count == 1 && _clientCount == 1)
                    {
                        //TODO 触发事件 (关闭客户端) 
                        Svision.GetMe().tcpStr = "客户端已断开" + DateTime.Now.ToLongTimeString().ToString() + System.Environment.NewLine ;
                        Svision.GetMe().ShowTCPMessage();
                        CloseSession(session);
                        RaiseNetError(session);
                        return;
                    }
                }
                catch (SocketException ex)//
                {
                    
                    //TODO 异常处理  
                    Svision.GetMe().tcpStr = "客户端已断开" + DateTime.Now.ToLongTimeString().ToString() + System.Environment.NewLine ;
                    Svision.GetMe().ShowTCPMessage();
                    RaiseNetError(session);
                    if (ex.ErrorCode == 10054 && _clients.Count == 1 && _clientCount == 1)
                    {
                        CloseSession(session);
                        RaiseNetError(session);
                        return;
                    }
                }

                finally
                {
                    //继续接收来自来客户端的数据  
                    if (IsRunning && client.Connected && SocketConnected(session.ClientSocket))///////////////////////////////////////////////////////////2016/8/23/Add by ZY
                    {///////////////////////////////////////////////////////////////////////////////////////////////////
                        client.BeginReceive(session.RecvDataBuffer, 0, session.RecvDataBuffer.Length, SocketFlags.None,
                           new AsyncCallback(HandleDataReceived), session);
                    }//////////////////////////////////////////////////////////////////////////////////////////////////////
                    
                }

            }
        }
#endregion

#region Send
        /// <summary>  
        /// 发送数据  
        /// </summary>  
        /// <param name="session">接收数据的客户端会话</param>  
        /// <param name="data">数据报文</param>  
        public void Send(Session session, byte[] data)
        {
            if (session.ClientSocket.Connected && SocketConnected(session.ClientSocket))
            {
                Send(session.ClientSocket, data);
            }
            
        }

        /// <summary>  
        /// 异步发送数据至指定的客户端  
        /// </summary>  
        /// <param name="client">客户端</param>  
        /// <param name="data">报文</param>  
        public void Send(Socket client, byte[] data)
        {
            if (!IsRunning)
                throw new InvalidProgramException("This TCP Scoket server has not been started.");

            if (client == null)
                throw new ArgumentNullException("client");

            if (data == null)
                throw new ArgumentNullException("data");
            client.BeginSend(data, 0, data.Length, SocketFlags.None,
             new AsyncCallback(SendDataEnd), client);
        }

        /// <summary>  
        /// 发送数据完成处理函数  
        /// </summary>  
        /// <param name="ar">目标客户端Socket</param>  
        private void SendDataEnd(IAsyncResult ar)
        {
            if (IsRunning && SocketConnected((Socket)ar.AsyncState))
            {
              ((Socket)ar.AsyncState).EndSend(ar);  
            }
            
        }
#endregion

#region Events
        /// <summary>  
        /// 接收到数据事件  
        /// </summary>  
        public event EventHandler<EventArgs> DataReceived;

        private void RaiseDataReceived(Session session)
        {
            if (DataReceived != null && SocketConnected(session.ClientSocket))
            {
                DataReceived(session, new AsyncEventArgs(session));
            }
        }

        /// <summary>  
        /// 与客户端的连接已建立事件  
        /// </summary>  
        public event EventHandler<AsyncEventArgs> ClientConnected;
        /// <summary>  
        /// 与客户端的连接已断开事件  
        /// </summary>  
        public event EventHandler<AsyncEventArgs> ClientDisconnected;

        /// <summary>  
        /// 触发客户端连接事件  
        /// </summary>  
        /// <param name="session"></param>  
        private void RaiseClientConnected(Session session)
        {
            if (ClientConnected != null && SocketConnected(session.ClientSocket))
            {
                ClientConnected(this, new AsyncEventArgs(session));
            }
        }
        /// <summary>  
        /// 触发客户端连接断开事件  
        /// </summary>  
        /// <param name="client"></param>  
        private void RaiseClientDisconnected(Socket client)
        {
            if (ClientDisconnected != null )
            {
                ClientDisconnected(this, new AsyncEventArgs("连接断开"));
            }
        }
        /// <summary>  
        /// 网络错误事件  
        /// </summary>  
        public event EventHandler<AsyncEventArgs> NetError;
        /// <summary>  
        /// 触发网络错误事件  
        /// </summary>  
        /// <param name="client"></param>  
        private void RaiseNetError(Session session)
        {
            if (NetError != null)
            {
                NetError(this, new AsyncEventArgs(session));
            }
        }

        /// <summary>  
        /// 异常事件  
        /// </summary>  
        public event EventHandler<AsyncEventArgs> ServerException;
        /// <summary>  
        /// 触发异常事件  
        /// </summary>  
        /// <param name="client"></param>  
        private void RaiseServerException(Session session)
        {
            if (ServerException != null)
            {
                ServerException(this, new AsyncEventArgs(session));
            }
        }
#endregion


#region Close
        /// <summary>  
        /// 关闭一个与客户端之间的会话  
        /// </summary>  
        /// <param name="closeClient">需要关闭的客户端会话对象</param>  
        public void CloseSession(Session session)
        {
            if (session != null)
            {
                session.Datagram = null;
                session.RecvDataBuffer = null;
                session.recvLen = 0;
                _clients.Remove(session);
                _clientCount=0;
                //TODO 触发关闭事件  
                session.Close();
            }
        }
        /// <summary>  
        /// 关闭所有的客户端会话,与所有的客户端连接会断开  
        /// </summary>  
        public void CloseAllClient()
        {
//             foreach (Session client in _clients)
//             {
//                 CloseSession(client);
//             }
            for (int i=0;i < _clients.Count; i++)
            {
                CloseSession(_clients[i]);
            }
            _clientCount = 0;
            _clients.Clear();
        }

        /// <summary>  
        /// Performs application-defined tasks associated with freeing,   
        /// releasing, or resetting unmanaged resources.  
        /// </summary>  
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>  
        /// Releases unmanaged and - optionally - managed resources  
        /// </summary>  
        /// <param name="disposing"><c>true</c> to release   
        /// both managed and unmanaged resources; <c>false</c>   
        /// to release only unmanaged resources.</param>  
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    try
                    {
                        Stop();
                        if (_serverSock != null)
                        {
                            _serverSock = null;
                        }
                    }
                    catch (SocketException ex)
                    {
                        //TODO  
                        RaiseServerException(null);
                    }
                }
                disposed = true;
            }
        }
#endregion
    }  
}
