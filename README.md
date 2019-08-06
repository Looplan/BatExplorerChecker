# BatExplorerChecker

Small app, which deletes incomplete BatLogger records from an folder

## Incomplete BatLogger Records

So you might wonder what are incomplete BatLogger records? 
Well, the BatLogger puts out audio file (.wav) & metadata file (.xml) for each recording.
Both files have the same name but each file has an different extension, the files link to each other by using the same name.
You could say that an recording consists out of an audio file and an metadata file. 
If one of those files is missing we define the record as incomplete.
