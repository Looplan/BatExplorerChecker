# BatExplorerChecker

Small app, which deletes incomplete BatLogger records from an folder

## Why

Sometimes when employees of Loo Plan uploaded BatLogger data to the cloud, an part of the data would get lost due unknown reasons (as of 11/08/2019). So some BatLogger data was incomplete which poses some issues when importing in BatExplorer, it just wouldn't accept it. BatExplorerChecker was created to fix the incomplete BatLogger data by just removing the incomplete records. 

## Incomplete BatLogger Records

So you might wonder what are incomplete BatLogger records? 
Well, the BatLogger puts out audio file (.wav) & metadata file (.xml) for each recording.
Both files have the same name but each file has an different extension, the files link to each other by using the same name.
You could say that an recording consists out of an audio file and an metadata file. 
If one of those files is missing we define the record as incomplete.
