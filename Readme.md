<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128601357/12.2.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E3157)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/WindowsApplication54/Form1.cs) (VB: [Form1.vb](./VB/WindowsApplication54/Form1.vb))
<!-- default file list end -->
# How to implement custom XML serialization of a report that is bound to a dataset


<p>This example demonstrates the capability to serialize a report to XML along with its data source.</p>
<p>In this example, a report that is bound to a dataset can be saved to a file, and then restored along with its data source.</p>
<p>To do this, override the <strong>ReportStorageExtension</strong> class, and use the <strong>XtraReport.SaveLayoutToXml()</strong> method. And, you can use an opposite <strong>LoadLayoutFromXml()</strong> method, to de-serialize an XML report from a file or stream.</p>
<p>In addition, it is required to register a custom <strong>ReportDesignExtension</strong>, which implements the data source serialization functionality.</p>
<p>See also: <br /><a href="https://www.devexpress.com/Support/Center/p/E3169">How to serialize an XPO data source</a><br /><a href="https://www.devexpress.com/Support/Center/p/T269554"> How to serialize a report to XML with an untyped DataSet as a data source</a></p>

<br/>


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=reporting-winforms-custom-xml-serialization&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=reporting-winforms-custom-xml-serialization&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
