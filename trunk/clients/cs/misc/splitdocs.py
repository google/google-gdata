from sgmllib import SGMLParser
import urllib
import htmlentitydefs
import shutil
import os
import os.path
from os.path import join, getsize

global basePath
global targetPath
global pathVar
global subFolder
global allOldFiles

pathVar = 1


class URLLister(SGMLParser):
  def reset(self):
    SGMLParser.reset(self)
    self.urls = []
    
  def start_a(self, attrs):
    href = [v for k, v in attrs if k=='href'] 
    if href:
      self.urls.extend(href)


class BaseHTMLProcessor(SGMLParser): 
    def reset(self):                        
        self.pieces = [] 
        SGMLParser.reset(self) 
    def unknown_starttag(self, tag, attrs): 
        strattrs = "".join([' %s="%s"' % (key, value) for key, value in attrs]) 
        self.pieces.append("<%(tag)s%(strattrs)s>" % locals()) 
    def unknown_endtag(self, tag):          
        self.pieces.append("</%(tag)s>" % locals()) 
    def handle_charref(self, ref):          
        self.pieces.append("&#%(ref)s;" % locals()) 
    def handle_entityref(self, ref):        
        self.pieces.append("&%(ref)s" % locals()) 
        if htmlentitydefs.entitydefs.has_key(ref): 
            self.pieces.append(";") 
    def handle_data(self, text):            
        self.pieces.append(text) 
    def handle_comment(self, text):         
        self.pieces.append("<!--%(text)s-->" % locals()) 
    def handle_pi(self, text):              
        self.pieces.append("<?%(text)s>" % locals()) 
    def handle_decl(self, text): 
        self.pieces.append("<!%(text)s>" % locals()) 
        
    def start_a(self, attrs):
      strattrs = ""
      for key, value in attrs:
        if key == 'href':
          value = FileMover().Move(basePath, targetPath, value)
        strattrs += "".join(' %s="%s"' % (key, value))      
      self.pieces.append("<a %(strattrs)s>" % locals()) 

       
    def output(self):               
        """Return processed HTML as a single string""" 
        return "".join(self.pieces) 
        
      

class FileMover:
  def Move(self, base, target, currentFile):
    global pathVar
    relFile = currentFile
    src = base + currentFile
    if os.path.isfile(src):
      (dirName, fileName) = os.path.split(src)
      
      targetFolder = self.TestIfFileExists(fileName, target)
      if targetFolder == "":
          targetFolder = self.EnsureCorrectTargetPath(target, pathVar)
      dst = targetFolder + fileName
      shutil.copyfile(src, dst)
      relFile = "%s%s/%s" % (subFolder, pathVar, fileName)
    # now remove the basePath and return only the relative portion
    # which just assumes that the index is in the directory above
    return relFile;
    
  def TestIfFileExistsOld(self, fileName, target):
    for root, dirs, files in os.walk(target):
      if 'folder' in root:
        if fileName in files:
          return root
    return ""

  def TestIfFileExists(self, fileName, target):
    global allOldFiles
    for path, name in allOldFiles:
      if name == fileName:
        return path
    return ""

  
    
  def EnsureCorrectTargetPath(self, basePath, varPath):
    global pathVar
    testPath = "%s%s%s/" % (basePath, subFolder, varPath)
    if os.path.isdir(testPath):
      file_count = sum((len(f) for _, _, f in os.walk(testPath)))
      if file_count > 100:
        pathVar += 1
        return self.EnsureCorrectTargetPath(basePath, pathVar)
      return testPath
    os.mkdir(testPath)
    return testPath
        
    
basePath = "../docs/generated/"
targetPath = "../docs/generated/"
subFolder = "folder"

allOldFiles = []

#populate the list of the current files, all files in folderx subfolders
for root, dirs, files in os.walk(basePath): 
  if 'folder' in root:
    for f in files: 
      allOldFiles.append((root, f)) 


usock = urllib.urlopen("../docs/generated/Index.html")
parser = BaseHTMLProcessor()
parser.feed(usock.read())
usock.close()
parser.close()
print parser.output()

