'''
Created on Sep 21, 2011

@author: Geoff
'''
import clean
import tempfile
import shutil
import os
import re

def writeVersionToFile(filePath, major, minor, build, revision):
    fh, abs_path = tempfile.mkstemp()
    
    print 'Updating file {0} to version {1}.{2}.{3}.{4}'.format(filePath, major, minor, build, revision)
    new_file = open(abs_path, 'wb')
    old_file = open(filePath, 'rb')
    for line in old_file:
        if re.match(r'\[assembly: AssemblyFileVersion\("\d+\.\d+\.(\d+|\*)(\.(\d+|\*))?"\)\]', line):
            line = '[assembly: AssemblyFileVersion("{0}.{1}.{2}.{3}")]\r\n'.format(major, minor, build, revision)
        if re.match(r'\[assembly: AssemblyVersion\("\d+\.\d+\.(\d+|\*)(\.(\d+|\*))?"\)\]', line):
            line = '[assembly: AssemblyVersion("{0}.{1}.{2}.{3}")]\r\n'.format(major, minor, build, revision)

        new_file.write(line)
       
    old_file.close()
    new_file.close()
    os.close(fh)
    
    os.remove(filePath)
    shutil.move(abs_path, filePath)

def bumpVersion(args):
    version = []
    for idx in range(4):
        if idx < len(args):
            version.append(int(args[idx]))
        else:
            version.append(0)
    
    #Creating a closure that is returned to remove globals
    def updateFile(filePath):
        major, minor, build, revision = version
        writeVersionToFile(filePath, major, minor, build, revision)
        
    return updateFile 

if __name__ == '__main__':
    import sys

    version = [1, 0, 0, 0]
        
    if len(sys.argv) > 1:
        version = sys.argv[1].split('.')

    clean.recurseDir(os.getcwd(), 'AssemblyInfo.cs', bumpVersion(version))