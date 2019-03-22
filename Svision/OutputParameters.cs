using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;

namespace Svision
{
    public partial class OutputParameters : Form
    {
         HTuple showTempFlag;
        public OutputParameters()
        {
            InitializeComponent();
             showTempFlag = null;
            //for (int i = 19; i > 0; i--)
            //{
            //    if (UserCode.GetInstance().gProCd[i].FuncID != ProCodeCls.MainFunction.NullFBD
            //        && UserCode.GetInstance().gProCd[i].FuncID != ProCodeCls.MainFunction.OutputSerialOutputFBD)
            //    {
            //        UserCode.GetInstance().showCurIdx = i;
            //        break;
            //    }
            //}
        }

        private void checkListBoxOutputParameters_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
            
            if (e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
            {
                switch (UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].FuncID)
                {
                    case ProCodeCls.MainFunction.InputCameraInputFBD:
                        break;
                    case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                        break;
                    case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                        break;
                    case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                        break;
                    case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                        break;
                    case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                        {
                            UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gBP.showOutputResultFlag[showTempFlag[e.Index]] = false;
                        }
                        break;
                    case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                        {
                            UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gSSP.showOutputResultFlag[e.Index] = false;
                        }
                        break;
                    case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                        {
                            UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].tAShpSrh.showOutputResultFlag[e.Index] = false;
                        }
                        break;
                    default:
                        break;
                }

 
            }
            if (e.NewValue == CheckState.Checked && e.CurrentValue == CheckState.Unchecked)
            {
                switch (UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].FuncID)
                {
                    case ProCodeCls.MainFunction.InputCameraInputFBD:
                        break;
                    case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                        break;
                    case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                        break;
                    case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                        break;
                    case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                        break;
                    case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                        {
                            UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gBP.showOutputResultFlag[showTempFlag[e.Index]] = true;
                        }
                        break;
                    case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                        {
                            UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gSSP.showOutputResultFlag[e.Index] = true;
                        }
                        break;
                    case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                        {
                            UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].tAShpSrh.showOutputResultFlag[e.Index] = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OutputParameters_Load(object sender, EventArgs e)
        {
            try{
               switch (UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].FuncID)
            {
                case ProCodeCls.MainFunction.InputCameraInputFBD:
                    break;
                case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                    break;
                case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                    break;
                case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                    break;
                case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                    break;
                case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                    {
                        showTempFlag = 0;

                        if (UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gBP.showOutputResultFlag[0])
                        {
                            checkListBoxOutputParameters.Items.Add(UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gBP.showOutputResultStr[0], true);
                        }
                        else
                        {
                            checkListBoxOutputParameters.Items.Add(UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gBP.showOutputResultStr[0], false);
                        }



                        showTempFlag = showTempFlag.TupleConcat(1);
                        if (UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gBP.showOutputResultFlag[1])
                        {
                            checkListBoxOutputParameters.Items.Add(UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gBP.showOutputResultStr[1], true);
                        }
                        else
                        {
                            checkListBoxOutputParameters.Items.Add(UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gBP.showOutputResultStr[1], false);
                        }
                       
                            
                        
                        //checkListBoxOutputParameters.Items.Add(UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gBP.showOutputResultStr[0], true);
                        //checkListBoxOutputParameters.Items.Add(UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gBP.showOutputResultStr[1], true);
                        
                        for (int i = 0; i < 34; i++)
                        {
                            if (UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gBP.outputIDIsChecked[i])
                            {
                                showTempFlag = showTempFlag.TupleConcat(i + 2);
                                if (UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gBP.showOutputResultFlag[i+2])
                                {
                                    checkListBoxOutputParameters.Items.Add(UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gBP.showOutputResultStr[i+2], true);
                                } 
                                else
                                {
                                    checkListBoxOutputParameters.Items.Add(UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gBP.showOutputResultStr[i+2], false);
                                }

                                
                                   
                                

                            }

                        }
                    }
                    break;
                case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gSSP.showOutputResultFlag[i])
                            {
                                checkListBoxOutputParameters.Items.Add(UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gSSP.showOutputResultStr[i], true); 
                            } 
                            else
                            {
                                checkListBoxOutputParameters.Items.Add(UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gSSP.showOutputResultStr[i], false); 
                            }
                                                     
                        }
                    }
                    break;
                case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            if (UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gASSP.showOutputResultFlag[i])
                            {
                                checkListBoxOutputParameters.Items.Add(UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gASSP.showOutputResultStr[i], true);
                            }
                            else
                            {
                                checkListBoxOutputParameters.Items.Add(UserCode.GetInstance().gProCd[UserCode.GetInstance().showCurIdx].gASSP.showOutputResultStr[i], false);
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
            //checkListBoxOutputParameters.SetItemChecked(0, Svision.GetMe().showImageResult);
            //checkListBoxOutputParameters.SetItemChecked(1, Svision.GetMe().showScore);
            //checkListBoxOutputParameters.SetItemChecked(2, Svision.GetMe().showPosition);
            //checkListBoxOutputParameters.SetItemChecked(3, Svision.GetMe().showAngle);
            //checkListBoxOutputParameters.SetItemChecked(4, Svision.GetMe().showTime);
            //checkListBoxOutputParameters.SetItemChecked(5, Svision.GetMe().showClass);
        }
    }
}
