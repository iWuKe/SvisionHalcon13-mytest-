/**************************************************************************************
**
**       Filename:  SerialOutputFBDForm.cs
**
**    Description:  Serial output Function Block use for user code
**
**        Version:  1.0
**        Created:  2016-2-23
**       Revision:  v02.0007
**       Compiler:  vs2010
**        Company:  SIASUN
**
****************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Svision
{
    public enum DataType : int
    {
        BOOL_TYPE,              //  0
        SHORT_TYPE,             //  1
        INT_TYPE,               //  2
        FLOAT_TYPE,             //  3
        DOUBLE_TYPE             //  4
    }

    public partial class SerialOutputFBDForm : Form
    {
        public CodeOutputDic gCopd;
        private int tCurDataRow = 0;
        private int currentIndex;
        public int mainIndexSelectRow;
        //public bool[] findShapeModelBatchOperationFlag;
        //public string[] findShapeModelBatchOperationStr;
        //public bool[] findAnisoShapeModelBatchOperationFlag;
        //public string[] findAnisoShapeModelBatchOperationStr;
        //public bool[] blobAnalysisOperationFlag;
        //public string[] blobAnalysisOperationStr;
        public int flagModel;

        //public List<bool> blobAnalysisOperationFlagList;
        //public List<string> blobAnalysisOperationStrList;
        public class CodeOutputDic
        {
            public Dictionary<String, String[]> CodeOutputStr;
            public Dictionary<String, int> CodeOutputNum;

            public Dictionary<String, DataType> VariableType;
            public Dictionary<String, int> VariableIndex;
            public Dictionary<int, String> VariableIndexIntToString;

            public CodeOutputDic()
            {
                CodeOutputStr = new Dictionary<String, String[]>();
                CodeOutputNum = new Dictionary<String, int>();
                VariableType = new Dictionary<String, DataType>();
                VariableIndex = new Dictionary<String, int>();
                VariableIndexIntToString = new Dictionary<int, String>();
            }
        }


        
        public SerialOutputFBDForm(int cIdx)
        {
            try
            {

                currentIndex = cIdx;
                InitializeComponent();
                InitCom();
                LoadConfigParameterFromCode();
                LoadCodeParameterFromCode();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitCom()
        {
            gCopd = new CodeOutputDic();
            ListViewTreeSerialDataInit();

            for (int i = 0; i < 20; i++)
            {
                ProCodeCls.MainFunction tmpFuncID = UserCode.GetInstance().gProCd[i].FuncID;
                if ((tmpFuncID == ProCodeCls.MainFunction.MeasureShapeSearchFBD))
                {
                    comboBoxSerialOutputExpressionMain.Items.Add(String.Format("{0:D2}", i) + " " +
                        UserCode.GetInstance().codeInfoValToKey[tmpFuncID]);                 
                    
                }
                if ((tmpFuncID == ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD))
                {
                    comboBoxSerialOutputExpressionMain.Items.Add(String.Format("{0:D2}", i) + " " +
                        UserCode.GetInstance().codeInfoValToKey[tmpFuncID]);
                }
                if ((tmpFuncID == ProCodeCls.MainFunction.MeasureBlobAnalysisFBD))
                {
                    comboBoxSerialOutputExpressionMain.Items.Add(String.Format("{0:D2}", i) + " " +
                        UserCode.GetInstance().codeInfoValToKey[tmpFuncID]);
                    //String tmpnum = ;
                    //int codeRow = int.Parse(comboBoxSerialOutputExpressionMain.SelectedItem.ToString().Split(' ')[0]);
                }
            }
        }

        private void ListViewTreeSerialDataInit()
        {
            dataGridViewSerialOutputComData.Columns.Add("no", "序号");
            dataGridViewSerialOutputComData.Columns.Add("data", "数据");
            dataGridViewSerialOutputComData.Columns.Add("note", "注释");
            dataGridViewSerialOutputComData.Columns["no"].Width = (int)(dataGridViewSerialOutputComData.Width * 0.15);
            dataGridViewSerialOutputComData.Columns["data"].Width = (int)(dataGridViewSerialOutputComData.Width * 0.65);
            dataGridViewSerialOutputComData.Columns["note"].Width = (int)(dataGridViewSerialOutputComData.Width * 0.20);

            dataGridViewSerialOutputComData.Font = new Font("宋体", 12);
            intListViewTreeRowNum();

            dataGridViewSerialOutputComData.ColumnHeadersHeight = (int)(dataGridViewSerialOutputComData.Height * 0.125);
        }
        private void intListViewTreeRowNum(int rowNum = 8)
        {
            dataGridViewSerialOutputComData.RowCount = rowNum;
            dataGridViewSerialOutputComData.Columns["no"].ReadOnly = true;
            dataGridViewSerialOutputComData.Columns["data"].ReadOnly = true;
            for (int i = 0; i < rowNum; i++)
            {
                dataGridViewSerialOutputComData.Rows[i].Height = (int)(dataGridViewSerialOutputComData.Height * 0.125);
                dataGridViewSerialOutputComData.Rows[i].Cells[0].Value = i + 1;
            }
        }
        private void LoadConfigParameterFromCode()
        {
            radioButtonSerialOutputComModeEthernet.Checked = UserCode.GetInstance().gProCd[currentIndex].gSOP.isGige;
            radioButtonSerialOutputComModeRS232.Checked = !UserCode.GetInstance().gProCd[currentIndex].gSOP.isGige;
            radioButtonSerialOutputFormatAscii.Checked = UserCode.GetInstance().gProCd[currentIndex].gSOP.outputForm;
            radioButtonSerialOutputFormatBin.Checked = !UserCode.GetInstance().gProCd[currentIndex].gSOP.outputForm;
            radioButtonSerialOutputNegativeMinus.Checked = UserCode.GetInstance().gProCd[currentIndex].gSOP.NegativeMinus;
            radioButtonSerialOutputNegative8.Checked = !UserCode.GetInstance().gProCd[currentIndex].gSOP.NegativeMinus;
            radioButtonSerialOutputEraseZeroYes.Checked = UserCode.GetInstance().gProCd[currentIndex].gSOP.EraseZeroYes;
            radioButtonSerialOutputEraseZeroNo.Checked = !UserCode.GetInstance().gProCd[currentIndex].gSOP.EraseZeroYes;
            comboBoxSerialOutputIntBit.SelectedIndex = UserCode.GetInstance().gProCd[currentIndex].gSOP.intBit - 1;
            comboBoxSerialOutputFloatBit.SelectedIndex = UserCode.GetInstance().gProCd[currentIndex].gSOP.floatBit;
            comboBoxOutputFieldSeparator.SelectedIndex = UserCode.GetInstance().gProCd[currentIndex].gSOP.FieldSeparator;
            comboBoxSerialOutputRecordSeparator.SelectedIndex = UserCode.GetInstance().gProCd[currentIndex].gSOP.RecordSeparator;
        }

        private void LoadCodeParameterFromCode()
        {
            try
            {
                if (UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList.Count != 0)
                {
                    if (ProCodeCls.MainFunction.NullFBD != (ProCodeCls.MainFunction)UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[0].funcID)
                    {

                        comboBoxSerialOutputExpressionMain.SelectedIndex =
                            comboBoxSerialOutputExpressionMain.Items.IndexOf(String.Format("{0:D2}", UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[0].row) + " " +
                            UserCode.GetInstance().codeInfoValToKey[(ProCodeCls.MainFunction)UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[0].funcID]);

                    }
                    for (int i = 0; i < UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList.Count; i++)
                    {
                        if (UserCode.GetInstance().gProCd[UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].row].FuncID
                            == (ProCodeCls.MainFunction)UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].funcID)
                        {
                            if (ProCodeCls.MainFunction.NullFBD == (ProCodeCls.MainFunction)UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].funcID)
                                continue;
                            String tmp = String.Format("{0:D2}", UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].row) + " " +
                                UserCode.GetInstance().codeInfoValToKey[(ProCodeCls.MainFunction)UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].funcID] + " ";

                            switch ((ProCodeCls.MainFunction)UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].funcID)
                            {
                                case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                                    if ((DataType)UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].datatype == DataType.DOUBLE_TYPE)
                                    {
                                        if (UserCode.GetInstance().gProCd[UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].row].gSSP.outPutStringList.Count > UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].idx)
                                        {
                                            tmp += UserCode.GetInstance().gProCd[UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].row].gSSP.outPutStringList[UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].idx];
                                            dataGridViewSerialOutputComData.Rows[i].Cells[1].Value = tmp;
                                        }

                                    }

                                    break;
                                case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                                    if ((DataType)UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].datatype == DataType.DOUBLE_TYPE)
                                    {
                                        if (UserCode.GetInstance().gProCd[UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].row].gASSP.outPutStringList.Count > UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].idx)
                                        {
                                            tmp += UserCode.GetInstance().gProCd[UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].row].gASSP.outPutStringList[UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].idx];
                                            dataGridViewSerialOutputComData.Rows[i].Cells[1].Value = tmp;
                                        }

                                    }

                                    break;
                                case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                                    if ((DataType)UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].datatype == DataType.DOUBLE_TYPE)
                                    {
                                        if (UserCode.GetInstance().gProCd[UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].row].gBP.outPutStringList.Count > UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].idx)
                                        {
                                            tmp += UserCode.GetInstance().gProCd[UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].row].gBP.outPutStringList[UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList[i].idx];
                                            dataGridViewSerialOutputComData.Rows[i].Cells[1].Value = tmp;
                                        
                                        }
                                       
                                    }

                                    break;

                            }

                            
                        }
                    }

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void radioButtonSerialOutputFormatBin_Click(object sender, EventArgs e)
        {
            comboBoxSerialOutputIntBit.Enabled = false;
            comboBoxSerialOutputFloatBit.Enabled = false;
            groupBoxSerialBoxNegative.Enabled = false;
            groupBoxSerialOutputEraseZero.Enabled = false;
            comboBoxOutputFieldSeparator.Enabled = false;
            comboBoxSerialOutputRecordSeparator.Enabled = false;
        }

        private void radioButtonSerialOutputFormatAscii_Click(object sender, EventArgs e)
        {
            comboBoxSerialOutputIntBit.Enabled = true;
            comboBoxSerialOutputFloatBit.Enabled = true;
            groupBoxSerialBoxNegative.Enabled = true;
            groupBoxSerialOutputEraseZero.Enabled = true;
            comboBoxOutputFieldSeparator.Enabled = true;
            comboBoxSerialOutputRecordSeparator.Enabled = true;
        }

        private void comboBoxSerialOutputExpressionMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                String tmp = comboBoxSerialOutputExpressionMain.SelectedItem.ToString().Substring(3);
                comboBoxSerialOutputExpressionSub.Items.Clear();
                comboBoxSerialOutputExpressionSub.Text = null;
                for (int i = 0; i < dataGridViewSerialOutputComData.RowCount; i++)
                {
                    dataGridViewSerialOutputComData.Rows[i].Cells[1].Value = null;
                }
                String tmpnum = comboBoxSerialOutputExpressionMain.SelectedItem.ToString().Split(' ')[0];
                int codeRow = int.Parse(tmpnum);
                mainIndexSelectRow = codeRow;
                switch (UserCode.GetInstance().gProCd[codeRow].FuncID)
                {
                    case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                        if (UserCode.GetInstance().gProCd[codeRow].gBP.outPutStringList.Count != 0)
                        {
                            String[] tBlobFbd = new String[UserCode.GetInstance().gProCd[codeRow].gBP.outPutStringList.Count];
                            for (int i = 0; i < UserCode.GetInstance().gProCd[codeRow].gBP.outPutStringList.Count; i++)
                            {
                                tBlobFbd[i] = UserCode.GetInstance().gProCd[codeRow].gBP.outPutStringList[i];
                            }
                            gCopd.CodeOutputStr.Clear();
                            gCopd.CodeOutputNum.Clear();
                            gCopd.VariableType.Clear();
                            gCopd.VariableIndex.Clear();
                            gCopd.CodeOutputStr.Add(UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureBlobAnalysisFBD], tBlobFbd);
                            gCopd.CodeOutputNum.Add(UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureBlobAnalysisFBD], tBlobFbd.Length);
                            for (int i = 0; i < UserCode.GetInstance().gProCd[codeRow].gBP.outPutStringList.Count; i++)
                            {
                                gCopd.VariableType.Add(UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureBlobAnalysisFBD] +
                                    " " + gCopd.CodeOutputStr[UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureBlobAnalysisFBD]][i], DataType.DOUBLE_TYPE);
                                gCopd.VariableIndex.Add(UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureBlobAnalysisFBD] +
                                    " " + gCopd.CodeOutputStr[UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureBlobAnalysisFBD]][i], i);
                            }

                            intListViewTreeRowNum(UserCode.GetInstance().gProCd[codeRow].gBP.regionNum * UserCode.GetInstance().gProCd[codeRow].gBP.selectItemCount + 1);
                            for (int i = 0; i < gCopd.CodeOutputNum[tmp]; i++)
                            {
                                comboBoxSerialOutputExpressionSub.Items.Add(gCopd.CodeOutputStr[tmp][i]);
                            }
                            comboBoxSerialOutputExpressionSub.SelectedIndex = 0;

                            checkedListBoxBatchOperation.Items.Clear();
                            for (int o = 0; o < UserCode.GetInstance().gProCd[codeRow].gBP.blobAnalysisOperationFlag.Length; o++)
                            {
                                checkedListBoxBatchOperation.Items.Add(UserCode.GetInstance().gProCd[codeRow].gBP.blobAnalysisOperationStr[o], UserCode.GetInstance().gProCd[codeRow].gBP.blobAnalysisOperationFlag[o]);
                            }
                            flagModel = 0;
                            int temp = (int)numericUpDownRecycleNumber.Value;
                            numericUpDownRecycleNumber.Value = 0;
                            numericUpDownRecycleNumber.Maximum = (decimal)UserCode.GetInstance().gProCd[codeRow].gBP.regionNum;
                            if (temp > UserCode.GetInstance().gProCd[codeRow].gBP.regionNum )
                            {
                                numericUpDownRecycleNumber.Value = (decimal)UserCode.GetInstance().gProCd[codeRow].gBP.regionNum;
                            }
                            else
                            {
                                numericUpDownRecycleNumber.Value = (decimal)temp;
                            }
                            

                        }
                        break;
                    case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                        if (UserCode.GetInstance().gProCd[codeRow].gSSP.outPutStringList.Count != 0)
                        {
                            String[] tShapeSearchFbd = new String[UserCode.GetInstance().gProCd[codeRow].gSSP.outPutStringList.Count];
                            for (int i = 0; i < UserCode.GetInstance().gProCd[codeRow].gSSP.outPutStringList.Count; i++)
                            {
                                tShapeSearchFbd[i] = UserCode.GetInstance().gProCd[codeRow].gSSP.outPutStringList[i];
                            }
                            gCopd.CodeOutputStr.Clear();
                            gCopd.CodeOutputNum.Clear();
                            gCopd.VariableType.Clear();
                            gCopd.VariableIndex.Clear();
                            gCopd.CodeOutputStr.Add(UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureShapeSearchFBD], tShapeSearchFbd);
                            gCopd.CodeOutputNum.Add(UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureShapeSearchFBD], tShapeSearchFbd.Length);
                            for (int i = 0; i < UserCode.GetInstance().gProCd[codeRow].gSSP.outPutStringList.Count; i++)
                            {
                                gCopd.VariableType.Add(UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureShapeSearchFBD] +
                                    " " + gCopd.CodeOutputStr[UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureShapeSearchFBD]][i], DataType.DOUBLE_TYPE);
                                gCopd.VariableIndex.Add(UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureShapeSearchFBD] +
                                    " " + gCopd.CodeOutputStr[UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureShapeSearchFBD]][i], i);
                            }

                            intListViewTreeRowNum(UserCode.GetInstance().gProCd[codeRow].gSSP.Max_Object_Num * 5 + 1);
                            for (int i = 0; i < gCopd.CodeOutputNum[tmp]; i++)
                            {
                                comboBoxSerialOutputExpressionSub.Items.Add(gCopd.CodeOutputStr[tmp][i]);
                            }
                            comboBoxSerialOutputExpressionSub.SelectedIndex = 0;
                            checkedListBoxBatchOperation.Items.Clear();
                            for (int o = 0; o < UserCode.GetInstance().gProCd[codeRow].gSSP.findShapeModelBatchOperationFlag.Length; o++)
                            {
                                checkedListBoxBatchOperation.Items.Add(UserCode.GetInstance().gProCd[codeRow].gSSP.findShapeModelBatchOperationStr[o], UserCode.GetInstance().gProCd[codeRow].gSSP.findShapeModelBatchOperationFlag[o]);
                            }
                            flagModel = 1;
                            int temp = (int)numericUpDownRecycleNumber.Value;
                            numericUpDownRecycleNumber.Value = 0;
                            numericUpDownRecycleNumber.Maximum = (decimal)UserCode.GetInstance().gProCd[codeRow].gSSP.Max_Object_Num;
                            if (temp > UserCode.GetInstance().gProCd[codeRow].gSSP.Max_Object_Num)
                            {
                                numericUpDownRecycleNumber.Value = (decimal)UserCode.GetInstance().gProCd[codeRow].gSSP.Max_Object_Num;
                            }
                            else
                            {
                                numericUpDownRecycleNumber.Value = (decimal)temp;
                            }
                        }

                        break;
                    case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                        if (UserCode.GetInstance().gProCd[codeRow].gASSP.outPutStringList.Count != 0)
                        {
                            String[] tAShapeSearchFbd = new String[UserCode.GetInstance().gProCd[codeRow].gASSP.outPutStringList.Count];
                            for (int i = 0; i < UserCode.GetInstance().gProCd[codeRow].gASSP.outPutStringList.Count; i++)
                            {
                                tAShapeSearchFbd[i] = UserCode.GetInstance().gProCd[codeRow].gASSP.outPutStringList[i];
                            }
                            gCopd.CodeOutputStr.Clear();
                            gCopd.CodeOutputNum.Clear();
                            gCopd.VariableType.Clear();
                            gCopd.VariableIndex.Clear();
                            gCopd.CodeOutputStr.Add(UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD], tAShapeSearchFbd);
                            gCopd.CodeOutputNum.Add(UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD], tAShapeSearchFbd.Length);
                            for (int i = 0; i < UserCode.GetInstance().gProCd[codeRow].gASSP.outPutStringList.Count; i++)
                            {
                                gCopd.VariableType.Add(UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD] +
                                    " " + gCopd.CodeOutputStr[UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD]][i], DataType.DOUBLE_TYPE);
                                gCopd.VariableIndex.Add(UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD] +
                                    " " + gCopd.CodeOutputStr[UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD]][i], i);
                            }

                            intListViewTreeRowNum(UserCode.GetInstance().gProCd[codeRow].gASSP.Max_Object_Num * 7 + 1);
                            for (int i = 0; i < gCopd.CodeOutputNum[tmp]; i++)
                            {
                                comboBoxSerialOutputExpressionSub.Items.Add(gCopd.CodeOutputStr[tmp][i]);
                            }
                            comboBoxSerialOutputExpressionSub.SelectedIndex = 0;
                            checkedListBoxBatchOperation.Items.Clear();
                            for (int o = 0; o < UserCode.GetInstance().gProCd[codeRow].gASSP.findAnisoShapeModelBatchOperationFlag.Length; o++)
                            {
                                checkedListBoxBatchOperation.Items.Add(UserCode.GetInstance().gProCd[codeRow].gASSP.findAnisoShapeModelBatchOperationStr[o], UserCode.GetInstance().gProCd[codeRow].gASSP.findAnisoShapeModelBatchOperationFlag[o]);
                            }
                            flagModel = 2;
                            int temp = (int)numericUpDownRecycleNumber.Value;
                            numericUpDownRecycleNumber.Value = 0;
                            numericUpDownRecycleNumber.Maximum = (decimal)UserCode.GetInstance().gProCd[codeRow].gASSP.Max_Object_Num ;
                            if (temp > UserCode.GetInstance().gProCd[codeRow].gASSP.Max_Object_Num )
                            {
                                numericUpDownRecycleNumber.Value = (decimal)UserCode.GetInstance().gProCd[codeRow].gASSP.Max_Object_Num ;
                            }
                            else
                            {
                                numericUpDownRecycleNumber.Value = (decimal)temp;
                            }
                        }

                        break;
                }


            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            /********************************************OLD**************************************************/
            //             String tmp = comboBoxSerialOutputExpressionMain.SelectedItem.ToString().Substring(3);
            //             comboBoxSerialOutputExpressionSub.Items.Clear();
            // 
            //             for (int i = 0; i < gCopd.CodeOutputNum[tmp]; i++)
            //             {
            //                 comboBoxSerialOutputExpressionSub.Items.Add(gCopd.CodeOutputStr[tmp][i]);
            //             }
            //             comboBoxSerialOutputExpressionSub.SelectedIndex = 0;
            /*************************************************************************************************/
        }

        private void buttonSerialOutputInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if ((comboBoxSerialOutputExpressionMain.Text == String.Empty)
                    | (comboBoxSerialOutputExpressionSub.Text == String.Empty))
                {
                    MessageBox.Show("请选择正确的发送数据");
                }

                dataGridViewSerialOutputComData.Rows[tCurDataRow].Cells[1].Value = comboBoxSerialOutputExpressionMain.Text
                    + " " + comboBoxSerialOutputExpressionSub.Text;

                dataGridViewSerialOutputComData.CurrentCell = dataGridViewSerialOutputComData.Rows[tCurDataRow + 1].Cells[1];
                tCurDataRow = dataGridViewSerialOutputComData.CurrentRow.Index;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSerialOutputDelete_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewSerialOutputComData.Rows[tCurDataRow].Cells[1].Value = null;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewSerialOutputComData_CellClick(object sender, EventArgs e)
        {
            tCurDataRow = dataGridViewSerialOutputComData.CurrentRow.Index;
        }

        private void buttonSerialOutputConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                SaveSerialOutputProgram();
                SaveSerialOutputConfig();
                if (UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList.Count!=0)
                {
                    UserCode.GetInstance().isOverFlag[currentIndex] = 32;
                }
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveSerialOutputConfig()
        {
            UserCode.GetInstance().gProCd[currentIndex].gSOP.isGige = radioButtonSerialOutputComModeEthernet.Checked;
            UserCode.GetInstance().gProCd[currentIndex].gSOP.outputForm = radioButtonSerialOutputFormatAscii.Checked;
            if (radioButtonSerialOutputFormatAscii.Checked)
            {
                UserCode.GetInstance().gProCd[currentIndex].gSOP.NegativeMinus = radioButtonSerialOutputNegativeMinus.Checked;
                UserCode.GetInstance().gProCd[currentIndex].gSOP.EraseZeroYes = radioButtonSerialOutputEraseZeroYes.Checked;
                UserCode.GetInstance().gProCd[currentIndex].gSOP.intBit = (short)(comboBoxSerialOutputIntBit.SelectedIndex + 1);
                UserCode.GetInstance().gProCd[currentIndex].gSOP.floatBit = (short)comboBoxSerialOutputFloatBit.SelectedIndex;
                UserCode.GetInstance().gProCd[currentIndex].gSOP.FieldSeparator = (short)comboBoxOutputFieldSeparator.SelectedIndex;
                UserCode.GetInstance().gProCd[currentIndex].gSOP.RecordSeparator = (short)comboBoxSerialOutputRecordSeparator.SelectedIndex;
            }
        }

        private void SaveSerialOutputProgram()
        {
            try
            {
                String tmpStr;
                //int tIdx = 0;
                UserCode.GetInstance().gProCd[currentIndex].gSOP.sendDataInfoList.Clear();
                for (int i = 0; i < this.dataGridViewSerialOutputComData.RowCount; i++)
                {
                    if (this.dataGridViewSerialOutputComData.Rows[i].Cells[1].Value == null)
                        continue;
                    tmpStr = this.dataGridViewSerialOutputComData.Rows[i].Cells[1].Value.ToString();
                    int i1, i2, i3, i4;
                    i1 = int.Parse(tmpStr.Split(' ')[0]);
                    i2 = (int)UserCode.GetInstance().codeInfo[tmpStr.Split(' ')[1]];
                    i3 = (int)gCopd.VariableType[tmpStr.Split(' ')[1] + " " + tmpStr.Split(' ')[2]];
                    i4 = gCopd.VariableIndex[tmpStr.Split(' ')[1] + " " + tmpStr.Split(' ')[2]];
                    UserCode.GetInstance().gProCd[currentIndex].gSOP.addSendDataInfoListPara(i1, i2, i3, i4);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSerialOutputCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewSerialOutputComData_SizeChanged(object sender, EventArgs e)
        {
            if (dataGridViewSerialOutputComData != null)
            {
                dataGridViewSerialOutputComData.Columns["no"].Width = (int)(dataGridViewSerialOutputComData.Width * 0.15);
                dataGridViewSerialOutputComData.Columns["data"].Width = (int)(dataGridViewSerialOutputComData.Width * 0.65);
                dataGridViewSerialOutputComData.Columns["note"].Width = (int)(dataGridViewSerialOutputComData.Width * 0.20);
            }

        }

        private void checkedListBoxBatchOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (flagModel)
                {
                    case 0:
                        {
                            for (int i = 0; i < UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag.Length; i++)
                            {
                                if (checkedListBoxBatchOperation.GetItemChecked(i))
                                {
                                    UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[i] = true;
                                }
                                else
                                {
                                    UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[i] = false;
                                }
                            }
                        }
                        break;
                    case 1:
                        {
                            for (int i = 0; i < UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag.Length; i++)
                            {
                                if (checkedListBoxBatchOperation.GetItemChecked(i))
                                {
                                    UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[i] = true;
                                }
                                else
                                {
                                    UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[i] = false;
                                }
                            }
                        }
                        break;
                    case 2:
                        {
                            for (int i = 0; i < UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag.Length; i++)
                            {
                                if (checkedListBoxBatchOperation.GetItemChecked(i))
                                {
                                    UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[i] = true;
                                }
                                else
                                {
                                    UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[i] = false;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            try
            {
                switch (flagModel)
                {
                    case 0:
                        {
                            int num = 0;
                            for (int i = 0; i < UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag.Length; i++)
                            {
                                if (checkedListBoxBatchOperation.GetSelected(i))
                                {
                                    num = i;
                                }                                
                            }
                            if (num!=0)
                            {
                                bool tempFlag;
                                string tempStr;
                                tempFlag = UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[num - 1];
                                tempStr = UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationStr[num - 1];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[num - 1] = UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[num];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationStr[num - 1] = UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationStr[num];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[num] = tempFlag;
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationStr[num] = tempStr;
                            }
                            checkedListBoxBatchOperation.Items.Clear();
                            for (int o = 0; o < UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag.Length; o++)
                            {
                                checkedListBoxBatchOperation.Items.Add(UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationStr[o], UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[o]);
                            }
                            if (num==0)
                            {
                                checkedListBoxBatchOperation.SetSelected(0, UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[0]);
                            } 
                            else
                            {
                                checkedListBoxBatchOperation.SetSelected(num - 1, UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[num - 1]);
                            }
                            
                        }
                        break;
                    case 1:
                        {
                            int num = 0;
                            for (int i = 0; i < UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag.Length; i++)
                            {
                                if (checkedListBoxBatchOperation.GetSelected(i))
                                {
                                    num = i;
                                }
                            }
                            if (num != 0)
                            {
                                bool tempFlag;
                                string tempStr;
                                tempFlag = UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[num - 1];
                                tempStr = UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationStr[num - 1];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[num - 1] = UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[num];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationStr[num - 1] = UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationStr[num];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[num] = tempFlag;
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationStr[num] = tempStr;
                            }
                            checkedListBoxBatchOperation.Items.Clear();
                            for (int o = 0; o < UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag.Length; o++)
                            {
                                checkedListBoxBatchOperation.Items.Add(UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationStr[o], UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[o]);
                            }
                            if (num == 0)
                            {
                                checkedListBoxBatchOperation.SetSelected(0, UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[0]);
                            }
                            else
                            {
                                checkedListBoxBatchOperation.SetSelected(num - 1, UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[num - 1]);
                            }
                        }
                        break;
                    case 2:
                        {
                            int num = 0;
                            for (int i = 0; i < UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag.Length; i++)
                            {
                                if (checkedListBoxBatchOperation.GetSelected(i))
                                {
                                    num = i;
                                }
                            }
                            if (num != 0)
                            {
                                bool tempFlag;
                                string tempStr;
                                tempFlag = UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[num - 1];
                                tempStr = UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationStr[num - 1];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[num - 1] = UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[num];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationStr[num - 1] = UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationStr[num];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[num] = tempFlag;
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationStr[num] = tempStr;
                            }
                            checkedListBoxBatchOperation.Items.Clear();
                            for (int o = 0; o < UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag.Length; o++)
                            {
                                checkedListBoxBatchOperation.Items.Add(UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationStr[o], UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[o]);
                            }
                            if (num == 0)
                            {
                                checkedListBoxBatchOperation.SetSelected(0, UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[0]);
                            }
                            else
                            {
                                checkedListBoxBatchOperation.SetSelected(num - 1, UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[num - 1]);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            try
            {
                switch (flagModel)
                {
                    case 0:
                        {
                            int num = 0;
                            for (int i = 0; i < UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag.Length; i++)
                            {
                                if (checkedListBoxBatchOperation.GetSelected(i))
                                {
                                    num = i;
                                }
                            }
                            if (num != UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag.Length - 1)
                            {
                                bool tempFlag;
                                string tempStr;
                                tempFlag = UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[num + 1];
                                tempStr = UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationStr[num + 1];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[num + 1] = UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[num];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationStr[num + 1] = UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationStr[num];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[num] = tempFlag;
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationStr[num] = tempStr;
                            }
                            checkedListBoxBatchOperation.Items.Clear();
                            for (int o = 0; o < UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag.Length; o++)
                            {
                                checkedListBoxBatchOperation.Items.Add(UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationStr[o], UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[o]);
                            }
                            if (num == UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag.Length - 1)
                            {
                                checkedListBoxBatchOperation.SetSelected(UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag.Length - 1, UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag.Length - 1]);
                            }
                            else
                            {
                                checkedListBoxBatchOperation.SetSelected(num + 1, UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[num + 1]);
                            }
                        }
                        break;
                    case 1:
                        {
                            int num = 0;
                            for (int i = 0; i < UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag.Length; i++)
                            {
                                if (checkedListBoxBatchOperation.GetSelected(i))
                                {
                                    num = i;
                                }
                            }
                            if (num != UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag.Length - 1)
                            {
                                bool tempFlag;
                                string tempStr;
                                tempFlag = UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[num + 1];
                                tempStr = UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationStr[num + 1];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[num + 1] = UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[num];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationStr[num + 1] = UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationStr[num];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[num] = tempFlag;
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationStr[num] = tempStr;
                            }
                            checkedListBoxBatchOperation.Items.Clear();
                            for (int o = 0; o < UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag.Length; o++)
                            {
                                checkedListBoxBatchOperation.Items.Add(UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationStr[o], UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[o]);
                            }
                            if (num == UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag.Length - 1)
                            {
                                checkedListBoxBatchOperation.SetSelected(UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag.Length - 1, UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag.Length - 1]);
                            }
                            else
                            {
                                checkedListBoxBatchOperation.SetSelected(num + 1, UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[num + 1]);
                            }
                        }
                        break;
                    case 2:
                        {
                            int num = 0;
                            for (int i = 0; i < UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag.Length; i++)
                            {
                                if (checkedListBoxBatchOperation.GetSelected(i))
                                {
                                    num = i;
                                }
                            }
                            if (num != UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag.Length - 1)
                            {
                                bool tempFlag;
                                string tempStr;
                                tempFlag = UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[num + 1];
                                tempStr = UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationStr[num + 1];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[num + 1] = UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[num];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationStr[num + 1] = UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationStr[num];
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[num] = tempFlag;
                                UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationStr[num] = tempStr;
                            }
                            checkedListBoxBatchOperation.Items.Clear();
                            for (int o = 0; o < UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag.Length; o++)
                            {
                                checkedListBoxBatchOperation.Items.Add(UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationStr[o], UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[o]);
                            }
                            if (num == UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag.Length - 1)
                            {
                                checkedListBoxBatchOperation.SetSelected(UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag.Length - 1, UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag.Length - 1]);
                            }
                            else
                            {
                                checkedListBoxBatchOperation.SetSelected(num + 1, UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[num + 1]);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonBatchOperation_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridViewSerialOutputComData.RowCount; i++)
                {
                    dataGridViewSerialOutputComData.Rows[i].Cells[1].Value = null;
                }
                switch (flagModel)
                {
                    case 0:
                        {
                            
                            if (checkBoxFirstFieldIsNumber.Checked)
                            {
                                int k = 1;
                                dataGridViewSerialOutputComData.Rows[0].Cells[1].Value = comboBoxSerialOutputExpressionMain.Text
                   + " " + "识别目标个数";
                                for (int i = 0; i < (int)numericUpDownRecycleNumber.Value; i++)
                                {
                                    for (int j = 0; j < UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag.Length; j++)
                                    {

                                        if (UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[j])
                                        {
                                            dataGridViewSerialOutputComData.Rows[k].Cells[1].Value = comboBoxSerialOutputExpressionMain.Text
                      + " " + "第" + i.ToString() + "个目标" + UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationStr[j];
                                            k++;
                                        }
                                        
                                    }
                                }
                                dataGridViewSerialOutputComData.CurrentCell = dataGridViewSerialOutputComData.Rows[0].Cells[1];
                                tCurDataRow = dataGridViewSerialOutputComData.CurrentRow.Index;
                            } 
                            else
                            {
                                int k = 0;
                                for (int i = 0; i < (int)numericUpDownRecycleNumber.Value; i++)
                                {
                                    for (int j = 0; j < UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag.Length; j++)
                                    {

                                        if (UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[j])
                                        {
                                            dataGridViewSerialOutputComData.Rows[k].Cells[1].Value = comboBoxSerialOutputExpressionMain.Text
                       + " " + "第" + i.ToString() + "个目标" + UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationStr[j];
                                            k++;
                                        }
                                        
                                    }
                                }
                                dataGridViewSerialOutputComData.CurrentCell = dataGridViewSerialOutputComData.Rows[0].Cells[1];
                                tCurDataRow = dataGridViewSerialOutputComData.CurrentRow.Index;
                            }
                           

                            
                        }
                        break;
                    case 1:
                        {

                            if (checkBoxFirstFieldIsNumber.Checked)
                            {
                                int k = 1;
                                dataGridViewSerialOutputComData.Rows[0].Cells[1].Value = comboBoxSerialOutputExpressionMain.Text
                   + " " + "识别目标个数";
                                for (int i = 0; i < (int)numericUpDownRecycleNumber.Value; i++)
                                {
                                    for (int j = 0; j < UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag.Length; j++)
                                    {

                                        if (UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[j])
                                        {
                                            dataGridViewSerialOutputComData.Rows[k].Cells[1].Value = comboBoxSerialOutputExpressionMain.Text
                      + " " + "第" + i.ToString() + "个目标" + UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationStr[j];

                                            k++;
                                        }
                                        
                                    }
                                }
                                dataGridViewSerialOutputComData.CurrentCell = dataGridViewSerialOutputComData.Rows[0].Cells[1];
                                tCurDataRow = dataGridViewSerialOutputComData.CurrentRow.Index;
                            }
                            else
                            {
                                int k = 0;
                                for (int i = 0; i < (int)numericUpDownRecycleNumber.Value; i++)
                                {
                                    for (int j = 0; j < UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag.Length; j++)
                                    {

                                        if (UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[j])
                                        {
                                            dataGridViewSerialOutputComData.Rows[k].Cells[1].Value = comboBoxSerialOutputExpressionMain.Text
                       + " " + "第" + i.ToString() + "个目标" + UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationStr[j];
                                            k++;
                                        }
                                        
                                    }
                                }
                                dataGridViewSerialOutputComData.CurrentCell = dataGridViewSerialOutputComData.Rows[0].Cells[1];
                                tCurDataRow = dataGridViewSerialOutputComData.CurrentRow.Index;
                            }
                        }
                        break;
                    case 2:
                        {
                            if (checkBoxFirstFieldIsNumber.Checked)
                            {
                                int k = 1;
                                dataGridViewSerialOutputComData.Rows[0].Cells[1].Value = comboBoxSerialOutputExpressionMain.Text
                   + " " + "识别目标个数";
                                for (int i = 0; i < (int)numericUpDownRecycleNumber.Value; i++)
                                {
                                    for (int j = 0; j < UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag.Length; j++)
                                    {

                                        if (UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[j])
                                        {
                                            dataGridViewSerialOutputComData.Rows[k].Cells[1].Value = comboBoxSerialOutputExpressionMain.Text
                      + " " + "第" + i.ToString() + "个目标" + UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationStr[j];
                                            k++;
                                        }
                                        
                                    }
                                }
                                dataGridViewSerialOutputComData.CurrentCell = dataGridViewSerialOutputComData.Rows[0].Cells[1];
                                tCurDataRow = dataGridViewSerialOutputComData.CurrentRow.Index;
                            }
                            else
                            {
                                int k = 0;
                                for (int i = 0; i < (int)numericUpDownRecycleNumber.Value; i++)
                                {
                                    for (int j = 0; j < UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag.Length; j++)
                                    {

                                        if (UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[j])
                                        {
                                            dataGridViewSerialOutputComData.Rows[k].Cells[1].Value = comboBoxSerialOutputExpressionMain.Text
                       + " " + "第" + i.ToString() + "个目标" + UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationStr[j];
                                            k++;
                                        }
                                        
                                    }
                                }
                                dataGridViewSerialOutputComData.CurrentCell = dataGridViewSerialOutputComData.Rows[0].Cells[1];
                                tCurDataRow = dataGridViewSerialOutputComData.CurrentRow.Index;
                            }
                        }
                        break;
                    default:
                        break;
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewSerialOutputComData.RowCount; i++)
            {
                dataGridViewSerialOutputComData.Rows[i].Cells[1].Value = null;
            }
        }

        private void checkedListBoxBatchOperation_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                switch (flagModel)
                {
                    case 0:
                        {
                            for (int i = 0; i < UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag.Length; i++)
                            {
                                if (checkedListBoxBatchOperation.GetItemChecked(i))
                                {
                                    UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[i] = true;
                                }
                                else
                                {
                                    UserCode.GetInstance().gProCd[mainIndexSelectRow].gBP.blobAnalysisOperationFlag[i] = false;
                                }
                            }
                        }
                        break;
                    case 1:
                        {
                            for (int i = 0; i < UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag.Length; i++)
                            {
                                if (checkedListBoxBatchOperation.GetItemChecked(i))
                                {
                                    UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[i] = true;
                                }
                                else
                                {
                                    UserCode.GetInstance().gProCd[mainIndexSelectRow].gSSP.findShapeModelBatchOperationFlag[i] = false;
                                }
                            }
                        }
                        break;
                    case 2:
                        {
                            for (int i = 0; i < UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag.Length; i++)
                            {
                                if (checkedListBoxBatchOperation.GetItemChecked(i))
                                {
                                    UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[i] = true;
                                }
                                else
                                {
                                    UserCode.GetInstance().gProCd[mainIndexSelectRow].gASSP.findAnisoShapeModelBatchOperationFlag[i] = false;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }






    }
}
