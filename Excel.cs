using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Text;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.POIFS.FileSystem;

/// <summary>
///Common 的摘要说明
/// </summary>
public class Excel
{
    /// <summary>
    /// DataTable数据导出Excel(WinForm专用接口)
    /// </summary>
    /// <param name="dt">datatable数据</param>
    ///<param name="filePath">路径（含文件名）</param>
    public static void FromDataTableToExcelForWin(DataTable dt, string sheetName, string filePath)
    {

        NPOI.HSSF.UserModel.HSSFWorkbook book = ReturnBook(dt, sheetName);
        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            book.Write(fs);
            book = null;
            fs.Dispose();
        }
    }



    /// <summary>
    /// 由DataTable导出Excel
    /// </summary>
    /// <param name="sourceTable">要导出数据的DataTable</param>
    /// <returns>Excel工作表</returns>
    private static HSSFWorkbook ReturnBook(DataTable sourceTable, string sheetName)
    {
        HSSFWorkbook workbook = new HSSFWorkbook();
        HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet(sheetName);
        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
        // handling header.
        foreach (DataColumn column in sourceTable.Columns)
            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
        // handling value.
        int rowIndex = 1;
        foreach (DataRow row in sourceTable.Rows)
        {
            HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
            foreach (DataColumn column in sourceTable.Columns)
            {
                dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
            }
            rowIndex++;
        }
        return workbook;
    }





    /// <summary>
    /// 由DataSet导出Excel
    /// </summary>
    /// <param name="sourceTable">要导出数据的DataTable</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel工作表</returns>
    private static Stream ReturnStreamForDS(DataSet sourceDs, string sheetName)
    {    //NPOI.SS.UserModel.ISheet  workbook = new
        HSSFWorkbook workbook = new HSSFWorkbook();
        MemoryStream ms = new MemoryStream();
        string[] sheetNames = sheetName.Split(',');
        for (int i = 0; i < sheetNames.Length; i++)
        {
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet(sheetNames[i]);
            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
            // handling header.
            foreach (DataColumn column in sourceDs.Tables[i].Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            // handling value.
            int rowIndex = 1;
            foreach (DataRow row in sourceDs.Tables[i].Rows)
            {
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in sourceDs.Tables[i].Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }
        }
        workbook.Write(ms);
        ms.Flush();
        ms.Position = 0;
        workbook = null;
        return ms;
    }






    /// <summary>
    /// 由DataTable导出Excel
    /// </summary>
    /// <param name="sourceTable">要导出数据的DataTable</param>
    /// <returns>Excel工作表</returns>
    private static Stream ReturnStreamForDataTable(DataTable sourceTable, string sheetName)
    {
        HSSFWorkbook workbook = new HSSFWorkbook();
        MemoryStream ms = new MemoryStream();
        HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet(sheetName);
        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
        // handling header.
        foreach (DataColumn column in sourceTable.Columns)
            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
        // handling value.
        int rowIndex = 1;
        foreach (DataRow row in sourceTable.Rows)
        {
            HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
            foreach (DataColumn column in sourceTable.Columns)
            {
                dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
            }
            rowIndex++;
        }
        workbook.Write(ms);
        ms.Flush();
        ms.Position = 0;
        sheet = null;
        headerRow = null;
        workbook = null;
        return ms;
    }



    /// <summary>
    /// 由Excel导入DataTable
    /// </summary>
    /// <param name="excelFilePath">Excel文件路径，为物理路径。</param>
    /// <param name="sheetName">Excel工作表名称</param>
    /// <param name="headerRowIndex">Excel表头行索引</param>
    /// <returns>DataTable</returns>
    public static DataTable FromExcelToDataTable(string excelFilePath, string sheetName, int headerRowIndex)
    {
        using (FileStream stream = System.IO.File.OpenRead(excelFilePath))
        {
            return ReturnDataTable(stream, sheetName, headerRowIndex);
        }
    }


    /// <summary>
    /// 由Excel导入DataTable
    /// </summary>
    /// <param name="excelFileStream">Excel文件流</param>
    /// <param name="sheetName">Excel工作表名称</param>
    /// <param name="headerRowIndex">Excel表头行索引</param>
    /// <returns>DataTable</returns>
    public static DataTable ReturnDataTable(Stream excelFileStream, string sheetName, int headerRowIndex)
    {
        HSSFWorkbook workbook = new HSSFWorkbook(excelFileStream);
        HSSFSheet sheet = (HSSFSheet)workbook.GetSheet(sheetName);
        DataTable table = new DataTable();
        HSSFRow headerRow = (HSSFRow)sheet.GetRow(headerRowIndex);
        int cellCount = headerRow.LastCellNum;
        for (int i = headerRow.FirstCellNum; i < cellCount; i++)
        {
            DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
            table.Columns.Add(column);
        }
        for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
        {
            HSSFRow row = (HSSFRow)sheet.GetRow(i);
            DataRow dataRow = table.NewRow();
            for (int j = row.FirstCellNum; j < cellCount; j++)
                dataRow[j] = row.GetCell(j).ToString();
        }
        excelFileStream.Close();
        workbook = null;
        sheet = null;
        return table;
    }


    /// <summary>
    /// 由Excel导入DataTable
    /// </summary>
    /// <param name="excelFilePath">Excel文件路径，为物理路径。</param>
    /// <param name="sheetName">Excel工作表索引</param>
    /// <param name="headerRowIndex">Excel表头行索引</param>
    /// <returns>DataTable</returns>
    public static DataTable FromExcelToDataTable1(string excelFilePath, int sheetIndex, int headerRowIndex)
    {
       
        using (FileStream stream = System.IO.File.OpenRead(excelFilePath))
        {
            return ImportDataTableFromExcel(stream, sheetIndex, headerRowIndex);
        }
    }


    /// <summary>
    /// 由Excel导入DataTable
    /// </summary>
    /// <param name="excelFileStream">Excel文件流</param>
    /// <param name="sheetName">Excel工作表索引</param>
    /// <param name="headerRowIndex">Excel表头行索引</param>
    /// <returns>DataTable</returns>
    public static DataTable ImportDataTableFromExcel(Stream excelFileStream, int sheetIndex, int headerRowIndex)
    {
        HSSFWorkbook workbook = new HSSFWorkbook(excelFileStream);
        HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(sheetIndex);
        DataTable table = new DataTable();
        HSSFRow headerRow = (HSSFRow)sheet.GetRow(headerRowIndex);
        int cellCount = headerRow.LastCellNum;
        for (int i = headerRow.FirstCellNum; i < cellCount; i++)
        {
            if (headerRow.GetCell(i) == null || headerRow.GetCell(i).StringCellValue.Trim() == "")
            {
                // 如果遇到第一个空列，则不再继续向后读取
                cellCount = i + 1;
                break;
            }
            DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
            table.Columns.Add(column);
        }
        for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
        {
            HSSFRow row = (HSSFRow)sheet.GetRow(i);
            if (row == null || row.GetCell(0) == null || row.GetCell(0).ToString().Trim() == "")
            {
                // 如果遇到第一个空行，则不再继续向后读取
                break;
            }
            DataRow dataRow = table.NewRow();
            for (int j = row.FirstCellNum; j < cellCount; j++)
            {
                dataRow[j] = row.GetCell(j);
            }
            table.Rows.Add(dataRow);
        }
        excelFileStream.Close();
        workbook = null;
        sheet = null;
        return table;
    }




    /// <summary>
    /// 由Excel导入DataSet，如果有多个工作表，则导入多个DataTable
    /// </summary>
    /// <param name="excelFilePath">Excel文件路径，为物理路径。</param>
    /// <param name="headerRowIndex">Excel表头行索引</param>
    /// <returns>DataSet</returns>
    public static DataSet FromExcelToDataSet(string excelFilePath, int headerRowIndex)
    {
       // POIFSFileSystem fs = newPOIFSFileSystem(new FileInputStream("d:\test.xls")); 
        using (FileStream stream = System.IO.File.OpenRead(excelFilePath))
        {
            return ImportDataSetFromExcel(stream, headerRowIndex);
        }
    }


    /// <summary>
    /// 由Excel导入DataSet，如果有多个工作表，则导入多个DataTable
    /// </summary>
    /// <param name="excelFileStream">Excel文件流</param>
    /// <param name="headerRowIndex">Excel表头行索引</param>
    /// <returns>DataSet</returns>
    public static DataSet ImportDataSetFromExcel(Stream excelFileStream, int headerRowIndex)
    {
        DataSet ds = new DataSet();
        HSSFWorkbook workbook = new HSSFWorkbook(excelFileStream);
        for (int a = 0, b = workbook.NumberOfSheets; a < b; a++)
        {
            HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(a);
            DataTable table = new DataTable();
            HSSFRow headerRow = (HSSFRow)sheet.GetRow(headerRowIndex);
            int cellCount = headerRow.LastCellNum;
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                if (headerRow.GetCell(i) == null || headerRow.GetCell(i).StringCellValue.Trim() == "")
                {
                    // 如果遇到第一个空列，则不再继续向后读取
                    cellCount = i + 1;
                    break;
                }
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                if (row == null || row.GetCell(0) == null || row.GetCell(0).ToString().Trim() == "")
                {
                    // 如果遇到第一个空行，则不再继续向后读取
                    break;
                }
                DataRow dataRow = table.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        dataRow[j] = row.GetCell(j).ToString();
                    }
                }
                table.Rows.Add(dataRow);
            }
            ds.Tables.Add(table);
        }
        excelFileStream.Close();
        workbook = null;
        return ds;
    }




    /// <summary>
    /// 将Excel的列索引转换为列名，列索引从0开始，列名从A开始。如第0列为A，第1列为B...
    /// </summary>
    /// <param name="index">列索引</param>
    /// <returns>列名，如第0列为A，第1列为B...</returns>
    public static string ConvertColumnIndexToColumnName(int index)
    {
        index = index + 1;
        int system = 26;
        char[] digArray = new char[100];
        int i = 0;
        while (index > 0)
        {
            int mod = index % system;
            if (mod == 0) mod = system;
            digArray[i++] = (char)(mod - 1 + 'A');
            index = (index - 1) / 26;
        }
        StringBuilder sb = new StringBuilder(i);
        for (int j = i - 1; j >= 0; j--)
        {
            sb.Append(digArray[j]);
        }
        return sb.ToString();
    }





    //当从Excel获取年月日时，会存在一定的问题，应该在一下代码中，可以想到存在的问题，所以我们可以写个方法封装一下：　
    /// <summary>
    /// 转化日期
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns></returns>
    public static DateTime ConvertDate(string date)
    {
        DateTime dt = new DateTime();
        string[] time = date.Split('-');
        int year = Convert.ToInt32(time[2]);
        int month = Convert.ToInt32(time[0]);
        int day = Convert.ToInt32(time[1]);
        string years = Convert.ToString(year);
        string months = Convert.ToString(month);
        string days = Convert.ToString(day);
        if (months.Length == 4)
        {
            dt = Convert.ToDateTime(date);
        }
        else
        {
            string rq = "";
            if (years.Length == 1)
            {
                years = "0" + years;
            }
            if (months.Length == 1)
            {
                months = "0" + months;
            }
            if (days.Length == 1)
            {
                days = "0" + days;
            }
            rq = "20" + years + "-" + months + "-" + days;
            dt = Convert.ToDateTime(rq);
        }
        return dt;
    }
}