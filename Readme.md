<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/Form1.cs) (VB: [Form1.vb](./VB/Form1.vb))
<!-- default file list end -->
# How to provide the ability to resize rows


<p><strong>Description</strong>:<br />
 I am trying to provide my end-users with the ability to change a specific row's height via the mouse. Is there a way to implement this feature? </p><p><strong>Solution</strong>:<br />
 The XtraGrid doesn’t provide such a feature automatically due to the fact that it doesn't cache any data internally. Thus it can’t store different row sizes.  However, you can implement this task yourself.  To do this you should handle the GridView's <strong>MouseDown</strong>, <strong>MouseUp</strong> and <strong>CalcRowHeight</strong> events.  Within the <strong>MouseDown</strong> and  the <strong>MouseUp</strong> events you should handle row size changing.  Within the <strong>CalcRowHeight</strong> event handler you should pass the valid row height for a specific row.  In the attached project you will find a sample project which demonstrates this task</p>

<br/>


