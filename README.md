# Fault-controlled-Rivers-Extractor
A  method for the automatic detection of fault-controlled rivers (FCRs) from drainage maps using spatial pattern matching.It mainly includes three functions:

1 Scene modeling with ARG
Click 【Scene modeling】 to set parameters in the triggered dialog.

[Input River]：the vector river system in ESRI Shapefile format

[From Field] and [To Field]:select the fields indicating the flow direction in the river data.

Both [CT] and [LT] are parameters used to construct the scene model.

[Output]: The folder where the output results are stored. There are three output results, which are the point feature files and line feature files in ESRI Shapefile format used to compose the scene model, and the scene model in XML data format.


2 Define River Pattern

Choose one pattern which user want to define, then Click the corresponding button. Set the parameters in the triggered dialog, the result is saved in XML data format.

3 River pattern matching

Click 【pattern matching】, set the parameters in the triggered dialog.

[River Pattern]：the river pattern in XML data format.

[Scene model]：the scene model in XML data format.

[River Data]：the vector river system

[Output]:the result in ESRI Shapefile format
