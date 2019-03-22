/**************************************************************************************
**
**       Filename:  NewProcessEdit.cs
**
**    Description:  the window for user edit user vision program
**
**        Version:  1.0
**        Created:  2016-1-26
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
    public partial class NewProcessEdit : Form
    {
        public string gCurrentTreeViewStr;              //选中树的字符串
        public int gCurrentDataGridRow;                 //当前选中的程序行

        private String[] FuncName;
        private DataTable mDt;

        public NewProcessEdit()
        {
            try
            {

                InitializeComponent();
                FuncName = new String[20];
                mDt = new DataTable();
                gCurrentDataGridRow = 0xFF;
                gCurrentTreeViewStr = "";

                UIInit();

                String tCode = "";
                for (int i = 0; i < 20; i++)
                {
                    if (UserCode.GetInstance().gProCd[i].FuncID == ProCodeCls.MainFunction.NullFBD)
                        continue;

                    this.DgvMainCode.Rows[i].Cells[1].Value =
                        UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[i].FuncID];
                }
                this.DgvMainCode.Rows[0].ReadOnly = true;
                if (Svision.GetMe().checkBoxIsFile.Checked)
                {
                    UserCode.GetInstance().gProCd[0].FuncID = ProCodeCls.MainFunction.InputFileInputFBD;
                    this.DgvMainCode.Rows[0].Cells[1].Value = UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.InputFileInputFBD];
                } 
                else
                {
                    UserCode.GetInstance().gProCd[0].FuncID = ProCodeCls.MainFunction.InputCameraInputFBD;
                    this.DgvMainCode.Rows[0].Cells[1].Value = UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.InputCameraInputFBD];
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TreeNodeInit()
        {
            TreeNode InputNode, InputChildNode;
            TreeNode CalibrationNode, CalibrationChildNode;
            TreeNode MeasureNode, MeasureChildNode;
            TreeNode OutputNode, OutputChildNode;

            treeViewFuncBlk.Font = new Font("宋体", 15);

            //主功能分类
            InputNode = treeViewFuncBlk.Nodes.Add(Properties.Resources.InputUnit);
            CalibrationNode = treeViewFuncBlk.Nodes.Add(Properties.Resources.CalibrationUnit);
            MeasureNode = treeViewFuncBlk.Nodes.Add(Properties.Resources.MeasureUnit);
            OutputNode = treeViewFuncBlk.Nodes.Add(Properties.Resources.OutputUnit);

            //子功能
//输入单元
            InputNode.NodeFont = new Font("宋体", 15);
            InputChildNode = new TreeNode(Properties.Resources.InputUnitCameraInput);
            InputChildNode.NodeFont = new Font("宋体", 15);
            InputNode.Nodes.Add(InputChildNode);
            InputChildNode = new TreeNode(Properties.Resources.InputUnitFileInput);
            InputChildNode.NodeFont = new Font("宋体", 15);
            InputNode.Nodes.Add(InputChildNode);

//校准单元
            CalibrationNode.NodeFont = new Font("宋体", 15);

            CalibrationChildNode = new TreeNode(Properties.Resources.CalibrationUnitThreshold);
            CalibrationChildNode.NodeFont = new Font("宋体", 15);
            CalibrationNode.Nodes.Add(CalibrationChildNode);

            CalibrationChildNode = new TreeNode(Properties.Resources.CalibrationUnitBackgroundRemove);
            CalibrationChildNode.NodeFont = new Font("宋体", 15);
            CalibrationNode.Nodes.Add(CalibrationChildNode);

            //CalibrationChildNode = new TreeNode(Properties.Resources.CalibrationUnitHistogramEqualization);
            //CalibrationChildNode.NodeFont = new Font("宋体", 15);
            //CalibrationNode.Nodes.Add(CalibrationChildNode);
            //CalibrationChildNode = new TreeNode(Properties.Resources.CalibrationUnitSmooth);
            //CalibrationChildNode.NodeFont = new Font("宋体", 15);
            //CalibrationNode.Nodes.Add(CalibrationChildNode);
            //CalibrationChildNode = new TreeNode(Properties.Resources.CalibrationUnitColorConversion);
            //CalibrationChildNode.NodeFont = new Font("宋体", 15);
            //CalibrationNode.Nodes.Add(CalibrationChildNode);
            CalibrationChildNode = new TreeNode(Properties.Resources.CalibrationUnitMedianFilter);
            CalibrationChildNode.NodeFont = new Font("宋体", 15);
            CalibrationNode.Nodes.Add(CalibrationChildNode);
            //CalibrationChildNode = new TreeNode(Properties.Resources.CalibrationUnitFreqDomainFilter);
            //CalibrationChildNode.NodeFont = new Font("宋体", 15);
            //CalibrationNode.Nodes.Add(CalibrationChildNode);
            //CalibrationChildNode = new TreeNode(Properties.Resources.CalibrationUnitSharpenFilter);
            //CalibrationChildNode.NodeFont = new Font("宋体", 15);
            //CalibrationNode.Nodes.Add(CalibrationChildNode);
            CalibrationChildNode = new TreeNode(Properties.Resources.CalibrationUnitMorphologyProcessing);
            CalibrationChildNode.NodeFont = new Font("宋体", 15);
            CalibrationNode.Nodes.Add(CalibrationChildNode);
            
//测量单元
            MeasureNode.NodeFont = new Font("宋体", 15);
            MeasureChildNode = new TreeNode(Properties.Resources.MeasureUnitShapeSearch);
            MeasureChildNode.NodeFont = new Font("宋体", 15);
            MeasureNode.Nodes.Add(MeasureChildNode);
            MeasureChildNode = new TreeNode(Properties.Resources.MeasureUnitAnisoShapeSearch);
            MeasureChildNode.NodeFont = new Font("宋体", 15);
            MeasureNode.Nodes.Add(MeasureChildNode);
            MeasureChildNode = new TreeNode(Properties.Resources.MeasureUnitBlobAnalasis);
            MeasureChildNode.NodeFont = new Font("宋体", 15);
            MeasureNode.Nodes.Add(MeasureChildNode);

            //MeasureChildNode = new TreeNode(Properties.Resources.L);
            //MeasureChildNode.NodeFont = new Font("宋体", 15);
            //MeasureNode.Nodes.Add(MeasureChildNode);
            
//输出单元
            OutputNode.NodeFont = new Font("宋体", 15);
            OutputChildNode = new TreeNode(Properties.Resources.OutputUnitSerialOutput);
            OutputChildNode.NodeFont = new Font("宋体", 15);
            OutputNode.Nodes.Add(OutputChildNode);
        }

        private void ListViewInit()
        {
            try
            {

                DgvMainCode.Columns.Add("no", "序号");
                DgvMainCode.Columns.Add("program", "程序");
                DgvMainCode.Columns["no"].Width = (int)(0.2 * DgvMainCode.Width);
                DgvMainCode.Columns["program"].Width = (int)(0.8 * DgvMainCode.Width);
                DgvMainCode.RowCount = 20;
                //DgvMainCode.RowHeadersWidth = DgvMainCode.Width;
                /*
                mDt.Columns.Add("程序");
                for (int i = 0; i < 20; i++ )
                {
                    DataRow newRow = mDt.NewRow();
                    newRow["程序"] = "123";
                    mDt.Rows.Add(newRow);
                }
                DgvMainCode.DataSource = mDt;
                */
                DgvMainCode.Columns["no"].ReadOnly = true;
                DgvMainCode.Columns["program"].ReadOnly = true;
                for (int i = 0; i < 20; i++)
                {
                    DgvMainCode.Rows[i].Height = (int)(0.125 * DgvMainCode.Height);
                    DgvMainCode.Rows[i].Cells[0].Value = i;
                }

                DgvMainCode.ColumnHeadersHeight = (int)(0.125 * DgvMainCode.Height);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UIInit()
        {
            TreeNodeInit();
            ListViewInit();
        }

        public void treeViewFuncBlk_NodeMouseClick()
        {
            //UserCode.GetInstance().gProCd[0].FuncID = 1;
        }

        private void treeViewFuncBlk_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (treeViewFuncBlk.SelectedNode != null)
                {
                    if (treeViewFuncBlk.SelectedNode.Text == Properties.Resources.InputUnit |
                        treeViewFuncBlk.SelectedNode.Text == Properties.Resources.CalibrationUnit |
                        treeViewFuncBlk.SelectedNode.Text == Properties.Resources.MeasureUnit |
                        treeViewFuncBlk.SelectedNode.Text == Properties.Resources.OutputUnit)
                    {
                        gCurrentTreeViewStr = "Null";
                    }
                    else
                    {
                        gCurrentTreeViewStr = treeViewFuncBlk.SelectedNode.Text;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);          
            }

        }

        private void DgvMainCode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                gCurrentDataGridRow = DgvMainCode.CurrentRow.Index;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgvMainCode_CellMouseDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (UserCode.GetInstance().gProCd[e.RowIndex].FuncID != ProCodeCls.MainFunction.NullFBD)
                {
                    switch (UserCode.GetInstance().gProCd[e.RowIndex].FuncID)
                    {

                        case ProCodeCls.MainFunction.InputCameraInputFBD:        //CameraInput
                            break;
                        case ProCodeCls.MainFunction.InputFileInputFBD:          //FileInput
                            break;

                        case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:      //Filter
                            BackgroundRemoveFBDForm tBackFbd = new BackgroundRemoveFBDForm(e.RowIndex);
                            tBackFbd.ShowDialog();
                            break;
                        case ProCodeCls.MainFunction.CalibrationThresholdFBD:      //Threshold
                            ThresholdFBDForm tThreFbd = new ThresholdFBDForm(e.RowIndex);
                            tThreFbd.ShowDialog();
                            break;
                        case ProCodeCls.MainFunction.MeasureShapeSearchFBD:      //Match
                            FindShapeModelFBDForm tFdMdFbd = new FindShapeModelFBDForm(e.RowIndex);
                            tFdMdFbd.ShowDialog();
                            break;
                        case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:      //Match
                            FindAnisoShapeModelFBDForm tFdAMdFbd = new FindAnisoShapeModelFBDForm(e.RowIndex);
                            tFdAMdFbd.ShowDialog();
                            break;
                        case ProCodeCls.MainFunction.OutputSerialOutputFBD:      //SerialCom
                            SerialOutputFBDForm tSerOptFbd = new SerialOutputFBDForm(e.RowIndex);
                            tSerOptFbd.ShowDialog();
                            break;
                        case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                            MedianFilterFBDForm tMeFltFbd = new MedianFilterFBDForm(e.RowIndex);
                            tMeFltFbd.ShowDialog();
                            break;
                        case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                            MorphologyProcessingFBDForm tMorFbd = new MorphologyProcessingFBDForm(e.RowIndex);
                            tMorFbd.ShowDialog();
                            break;
                        case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                            BlobAnalysisFBDForm tBlobFbd = new BlobAnalysisFBDForm(e.RowIndex);
                            tBlobFbd.ShowDialog();
                            break;
                        case ProCodeCls.MainFunction.MeasureSurfaceBasedMatchFBD:
                            SurfaceBasedMatchFBDForm tSBMFbd = new SurfaceBasedMatchFBDForm(e.RowIndex);
                            tSBMFbd.ShowDialog();
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (0 == gCurrentDataGridRow)
                {
                    MessageBox.Show("首行为图像输入，不可修改");
                    return;
                }

                if ((gCurrentDataGridRow != 0xFF) && (gCurrentTreeViewStr != "Null"))
                {
                    UserCode.GetInstance().isOverFlag[gCurrentDataGridRow] = 1;
                    this.DgvMainCode.Rows[gCurrentDataGridRow].Cells[1].Value = gCurrentTreeViewStr;
                    UserCode.GetInstance().gProCd[gCurrentDataGridRow].FuncID = UserCode.GetInstance().codeInfo[gCurrentTreeViewStr];
                    if (Svision.GetMe().listBoxProcess.Items.Count>gCurrentDataGridRow)
                    {
                        if (Svision.GetMe().listBoxProcess.Items[gCurrentDataGridRow].ToString()=="")
                        {
                            Svision.GetMe().listBoxProcess.SelectedIndex = 0;
                            Svision.GetMe().listBoxProcess.Items.RemoveAt(gCurrentDataGridRow);
                            Svision.GetMe().listBoxProcess.Items.Insert(gCurrentDataGridRow, gCurrentDataGridRow.ToString() + "." + gCurrentTreeViewStr);
                        }
                        else if (int.Parse(Svision.GetMe().listBoxProcess.Items[gCurrentDataGridRow].ToString().Split('.')[0])==gCurrentDataGridRow)
                        {
                            Svision.GetMe().listBoxProcess.SelectedIndex = 0;
                            Svision.GetMe().listBoxProcess.Items.RemoveAt(gCurrentDataGridRow);
                            Svision.GetMe().listBoxProcess.Items.Insert(gCurrentDataGridRow, gCurrentDataGridRow.ToString() + "." + gCurrentTreeViewStr);
                        } 
                        else
                        {
                            Svision.GetMe().listBoxProcess.Items.Insert(gCurrentDataGridRow, gCurrentDataGridRow.ToString() + "." + gCurrentTreeViewStr);
                        }
                        
                        
                    } 
                    else
                    {
                        Svision.GetMe().listBoxProcess.Items.Insert(gCurrentDataGridRow, gCurrentDataGridRow.ToString() + "." + gCurrentTreeViewStr);
                    }
                    
                    switch (UserCode.GetInstance().gProCd[gCurrentDataGridRow].FuncID)
                    {
                        case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                            UserCode.GetInstance().CalibrationBackgroundRemoveInit(gCurrentDataGridRow);
                            break;
                        case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                            UserCode.GetInstance().CalibrationMedianFilterInit(gCurrentDataGridRow);
                            break;
                        case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                            UserCode.GetInstance().CalibrationMorphologyProcessingInit(gCurrentDataGridRow);
                            break;
                        case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                            UserCode.GetInstance().isOverFlag[gCurrentDataGridRow] = 10;
                            UserCode.GetInstance().BlobAnalysisInit(gCurrentDataGridRow);
                            break;
                        case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                            UserCode.GetInstance().CalibrationThresholdInit(gCurrentDataGridRow);
                            break;
                        case ProCodeCls.MainFunction.OutputSerialOutputFBD:
                            UserCode.GetInstance().isOverFlag[gCurrentDataGridRow] = 31;
                            UserCode.GetInstance().OutputSerialOutputInit(gCurrentDataGridRow);
                            break;
                        case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                            UserCode.GetInstance().isOverFlag[gCurrentDataGridRow] = 11;
                            UserCode.GetInstance().ShapeSearchInit(gCurrentDataGridRow);
                            break;
                        case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                            UserCode.GetInstance().isOverFlag[gCurrentDataGridRow] = 12;
                            UserCode.GetInstance().AnisoShapeSearchInit(gCurrentDataGridRow);
                            break;
                        case ProCodeCls.MainFunction.MeasureSurfaceBasedMatchFBD://新添加的三维模板匹配
                            //UserCode.GetInstance().isOverFlag[gCurrentDataGridRow] = 19;
                            UserCode.GetInstance().SurfaceBasedMatchInit(gCurrentDataGridRow);
                            break;
                    }
                }
                for (int i = 19; i >= 0; i--)
                {
                    if (UserCode.GetInstance().gProCd[i].FuncID != ProCodeCls.MainFunction.NullFBD
                        && UserCode.GetInstance().gProCd[i].FuncID != ProCodeCls.MainFunction.OutputSerialOutputFBD)
                    {
                        UserCode.GetInstance().showCurIdx = i;
                        break;
                    }
                }
                Svision.GetMe().listBoxProcess.SelectedIndex = UserCode.GetInstance().showCurIdx;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (0 == gCurrentDataGridRow)
                {
                    MessageBox.Show("首行为图像输入，不可修改");
                    return;
                }
                UserCode.GetInstance().isOverFlag[gCurrentDataGridRow] = 0;
                if ((string)this.DgvMainCode.Rows[gCurrentDataGridRow].Cells[1].Value != 
                    UserCode.GetInstance().codeInfoValToKey[ProCodeCls.MainFunction.NullFBD])
                {
                    if (Svision.GetMe().listBoxProcess.Items[gCurrentDataGridRow].ToString() == "")
                    {
                        
                    }
                    else  if (int.Parse(Svision.GetMe().listBoxProcess.Items[gCurrentDataGridRow].ToString().Split('.')[0]) == gCurrentDataGridRow)
                    {
                        Svision.GetMe().listBoxProcess.SelectedIndex = 0;
                        Svision.GetMe().listBoxProcess.Items.RemoveAt(gCurrentDataGridRow);
                        Svision.GetMe().listBoxProcess.Items.Insert(gCurrentDataGridRow, "");
                    }
                    
                    UserCode.GetInstance().gProCd[gCurrentDataGridRow].clear();
                    this.DgvMainCode.Rows[gCurrentDataGridRow].Cells[1].Value = "";
                    UserCode.GetInstance().gProCd[gCurrentDataGridRow].FuncID = ProCodeCls.MainFunction.NullFBD;
                }
                for (int i = 19; i >= 0; i--)
                {
                    if (UserCode.GetInstance().gProCd[i].FuncID != ProCodeCls.MainFunction.NullFBD
                        && UserCode.GetInstance().gProCd[i].FuncID != ProCodeCls.MainFunction.OutputSerialOutputFBD)
                    {
                        UserCode.GetInstance().showCurIdx = i;
                        break;
                    }
                }
                Svision.GetMe().listBoxProcess.SelectedIndex = UserCode.GetInstance().showCurIdx;
            }
                catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgvMainCode_SizeChanged(object sender, EventArgs e)
        {
            if (DgvMainCode!=null)
            {

                DgvMainCode.Columns["no"].Width = (int)(0.2 * DgvMainCode.Width);
                DgvMainCode.Columns["program"].Width = (int)(0.8 * DgvMainCode.Width);
            }


        }





    }
}
