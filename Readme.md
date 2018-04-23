# How to implement custom XML serialization of a report that is bound to a dataset


<p>This example demonstrates the capability to serialize a report to XML along with its data source.</p>
<p>In this example, a report that is bound to a dataset can be saved to a file, and then restored along with its data source.</p>
<p>To do this, override the <strong>ReportStorageExtension</strong> class, and use the <strong>XtraReport.SaveLayoutToXml()</strong> method. And, you can use an opposite <strong>LoadLayoutFromXml()</strong> method, to de-serialize an XML report from a file or stream.</p>
<p>In addition, it is required to register a custom <strong>ReportDesignExtension</strong>, which implements the data source serialization functionality.</p>
<p>See also: <br /><a href="https://www.devexpress.com/Support/Center/p/E3169">How to serialize an XPO data source</a><br /><a href="https://www.devexpress.com/Support/Center/p/T269554"> How to serialize a report to XML with an untyped DataSet as a data source</a></p>

<br/>


