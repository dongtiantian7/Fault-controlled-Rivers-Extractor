# Fault-controlled-Rivers-Extractor
A  method for the automatic detection of fault-controlled rivers (FCRs) from drainage maps using spatial pattern matching.It mainly includes three functions:<br>  

1 Scene modeling with ARG<br>  
Click 【Scene modeling】 to set parameters in the triggered dialog.<br>
[Input River]：the vector river system in ESRI Shapefile format<br>  
[From Field] and [To Field]: select the fields indicating the flow direction in the river data.<br>  
Both [CT] and [LT] are parameters used to construct the scene model.<br>  
[Output]: The folder where the output results are stored. There are three output results, which are the point feature files and line feature files in ESRI Shapefile format used to compose the scene model, and the scene model in XML data format.<br>  <br>  


2 Define River Pattern<br>  
Choose one pattern which user want to define, then Click the corresponding button. Set the parameters in the triggered dialog, the result is saved in XML data format.<br> 


3 River pattern matching<br>  
Click 【pattern matching】, set the parameters in the triggered dialog.<br>  
[River Pattern]：the river pattern in XML data format.<br> 
[Scene model]：the scene model in XML data format.<br>  
[River Data]：the vector river system in ESRI Shapefile format<br>  
[Output]: the result in ESRI Shapefile format<br>  
