using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ebi_Cucumber_Back.Controllers
{
    public static class StringExtension
    {
        //Ebi changed this normal method to extension method which enables developers to add methods to existing types without creating a new derived type

        /// <summary>
        ///  StringExtension Method: Converts a decimal value in the format of string to words
        /// </summary>
        public static string ToWords(this string stringNumber)
        {
            String val = "", wholeNo = stringNumber, points = "", andStr = "", pointStr = "";
            String endStr = "";
            try
            {
                int decimalPlace = stringNumber.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = stringNumber.Substring(0, decimalPlace);
                    points = stringNumber.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = "and";// just to separate whole stringNumberers from points/cents   
                        endStr = "Cents";//Cents  
                        
                        //Ebi added this section to cut extera digits in Cents
                        //e.g., 0.3652 -> 0.36 Cents
                        switch (points.Length)
                            {
                            case 0:
                                points = "00";
                                break;
                            case 1:
                                points += "0";
                                break;
                            default:
                                points = points.Substring(0, 2);
                                break;
                        }
                        
                        //in the main source program, programmer uses a method ConvertDecimal for Cents but the result is not good.
                        //pointStr = points.ConvertDecimals(); ... For example, 0.325 is converted to three two five cents!
                        //To solve this minor problem, I (Ebi) used to recall ToWords() function for converting Cents to words. It is a recusrive call.

                        pointStr = " " + points.ToWords();
                        
                    }
                }
                val = String.Format("{0} {1}{2} {3}", ConvertWholeNumber(wholeNo).Trim() , andStr, pointStr, endStr).Replace("- ", "-").Replace(" and ", " dollars and ");
            }
            catch { }
            return val;
        }//end of ToMoney extention method

        //Ebi: again, extension method...
        private static string ConvertDecimals(this string stringNumber)
        {
                String cd = "", digit = "", engOne = "";
                for (int i = 0; i < stringNumber.Length; i++)
                {
                    digit = stringNumber[i].ToString();
                    if (digit.Equals("0"))
                    {
                        engOne = "Zero";
                    }
                    else
                    {
                        engOne = ones(digit);
                    }
                    cd += " " + engOne;
                }
                return cd;
        }//end of extension method ConvertDecimals 
        
        private static string ConvertWholeNumber(string stringNumber)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX   
                bool isDone = false;//test if already translated   
                double dblAmt = (Convert.ToDouble(stringNumber));
                //if ((dblAmt > 0) && number.StartsWith("0"))   
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric   
                    beginsZero = stringNumber.StartsWith("0");

                    int numDigits = stringNumber.Length;
                    int pos = 0;//store digit grouping   
                    String place = "";//digit grouping name:hundres,thousand,etc...   
                    switch (numDigits)
                    {
                        case 1://ones' range   
                            word = ones(stringNumber);
                            isDone = true;
                            break;
                        case 2://tens' range   
                            word = tens(stringNumber);
                            isDone = true;
                            break;
                        case 3://hundreds' range   
                            pos = (numDigits % 3) + 1;
                            place = " Hundred ";
                            break;
                        case 4://thousands' range   
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " Thousand ";
                            break;
                        case 7://millions' range   
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " Million ";
                            break;
                        case 10://Billions's range   
                        case 11:
                        case 12:
                            pos = (numDigits % 10) + 1;
                            place = " Billion ";
                            break;
                        //Ebi feels necessary to add extra case for Trillion
                        case 13:
                        case 14:
                        case 15:
                            pos = (numDigits % 13) + 1;
                            place = " Trillion ";
                            break;
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)   
                        if (stringNumber.Substring(0, pos) != "0" && stringNumber.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumber(stringNumber.Substring(0, pos)) + place + ConvertWholeNumber(stringNumber.Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumber(stringNumber.Substring(0, pos)) + ConvertWholeNumber(stringNumber.Substring(pos));
                        }
                    }
                    //ignore digit grouping names   
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }//end of method ConvertWholeNumber

        private static String tens(String stringNumber)
        {
            int _Number = Convert.ToInt32(stringNumber);
            String name = null;
            switch (_Number)
            {
                case 10:
                    name = "Ten";
                    break;
                case 11:
                    name = "Eleven";
                    break;
                case 12:
                    name = "Twelve";
                    break;
                case 13:
                    name = "Thirteen";
                    break;
                case 14:
                    name = "Fourteen";
                    break;
                case 15:
                    name = "Fifteen";
                    break;
                case 16:
                    name = "Sixteen";
                    break;
                case 17:
                    name = "Seventeen";
                    break;
                case 18:
                    name = "Eighteen";
                    break;
                case 19:
                    name = "Nineteen";
                    break;
                case 20:
                    name = "Twenty---";
                    break;
                case 30:
                    name = "Thirty---";
                    break;
                case 40:
                    name = "Fourty---";
                    break;
                case 50:
                    name = "Fifty---";
                    break;
                case 60:
                    name = "Sixty---";
                    break;
                case 70:
                    name = "Seventy---";
                    break;
                case 80:
                    name = "Eighty---";
                    break;
                case 90:
                    name = "Ninety---";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = tens(stringNumber.Substring(0, 1) + "0") + " " + ones(stringNumber.Substring(1));
                    }
                    break;
            }
            return name;
        }//end of method tens

        private static String ones(String stringNumber)
        {
            int _Number = Convert.ToInt32(stringNumber);
            String name = "";
            switch (_Number)
            {

                case 1:
                    name = "One";
                    break;
                case 2:
                    name = "Two";
                    break;
                case 3:
                    name = "Three";
                    break;
                case 4:
                    name = "Four";
                    break;
                case 5:
                    name = "Five";
                    break;
                case 6:
                    name = "Six";
                    break;
                case 7:
                    name = "Seven";
                    break;
                case 8:
                    name = "Eight";
                    break;
                case 9:
                    name = "Nine";
                    break;
            }
            return name;
        }//end of method ones

    }//end of class StringExtension


}