// Authors:
//   Rafael Mizrahi   <rafim@mainsoft.com>
//   Erez Lotan       <erezl@mainsoft.com>
//   Oren Gurfinkel   <oreng@mainsoft.com>
//   Ofer Borstein
// 
// Copyright (c) 2004 Mainsoft Co.
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using NUnit.Framework;
using System;
using System.Data;

using GHTUtils;
using GHTUtils.Base;

namespace tests.system_data_dll.System_Data
{
[TestFixture] public class DataView_FindRows_arrO : GHTBase
{
	[Test] public void Main()
	{
		DataView_FindRows_arrO tc = new DataView_FindRows_arrO();
		Exception exp = null;
		try
		{
			// Every Test must begin with BeginTest
			tc.BeginTest("NoName");
			tc.run();
		}
		catch(Exception ex)
		{
			exp = ex;
		}
		finally
		{
			// Every Test must End with EndTest
			tc.EndTest(exp);
		}
		// After test is ready, remove this line
	}

	public void run()
	{
		Exception exp = null;
		
		DataRowView[] dvArr = null;

		//create the source datatable
		DataTable dt = GHTUtils.DataProvider.CreateChildDataTable();

		//create the dataview for the table
		DataView dv = new DataView(dt);


		try
		{
			BeginCase("FindRows ,no sort - exception");
			try
			{
				dvArr = dv.FindRows(new object[] {"3","3-String1"});
			}
			catch (System.ArgumentException ex)
			{
				exp = ex;
			}
			Compare(exp.GetType().FullName,typeof(System.ArgumentException).FullName);
			exp=null;
		}
		catch(Exception ex)	{exp = ex;}
		finally	{EndCase(exp); exp = null;}
	

		dv.Sort = "String1,ChildId";
		try
		{
			BeginCase("Find = wrong sort, can not find");
			dvArr = dv.FindRows(new object[] {"3","3-String1"});
			Compare(dvArr.Length  ,0);
			exp=null;
		}
		catch(Exception ex)	{exp = ex;}
		finally	{EndCase(exp); exp = null;}

		dv.Sort = "ChildId,String1";

		//get expected results
		DataRow[] drExpected = dt.Select("ChildId=3 and String1='3-String1'");

		try
		{
			BeginCase("FindRows - check count");
			dvArr = dv.FindRows(new object[] {"3","3-String1"});
			Compare(dvArr.Length,drExpected.Length );
		}
		catch(Exception ex)	{exp = ex;}
		finally	{EndCase(exp); exp = null;}

		try
		{
			BeginCase("FindRows - check data");
			
			//check that result is ok
			bool Succeed = true;
			for (int i=0; i<dvArr.Length ; i++)
			{
				Succeed = (int)dvArr[i]["ChildId"] == (int)drExpected [i]["ChildId"];
				if (!Succeed) break;
			}
			Compare(Succeed ,true);
		}
		catch(Exception ex)	{exp = ex;}
		finally	{EndCase(exp); exp = null;}

	}

	//Activate This Construntor to log All To Standard output
	//public TestClass():base(true){}

	//Activate this constructor to log Failures to a log file
	//public TestClass(System.IO.TextWriter tw):base(tw, false){}

	//Activate this constructor to log All to a log file
	//public TestClass(System.IO.TextWriter tw):base(tw, true){}

	//BY DEFAULT LOGGING IS DONE TO THE STANDARD OUTPUT ONLY FOR FAILURES

}
}