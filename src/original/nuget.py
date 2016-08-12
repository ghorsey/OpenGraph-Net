import fnmatch
import os
import shutil

def walklevel(some_dir, level=1):
    some_dir = some_dir.rstrip(os.path.sep)
    assert os.path.isdir(some_dir)
    num_sep = some_dir.count(os.path.sep)
    for root, dirs, files in os.walk(some_dir):
        yield root, dirs, files
        num_sep_this = root.count(os.path.sep)
        if num_sep + level <= num_sep_this:
            del dirs[:]

matches = []
nugetPath = ".\\.nuget\\nuget.exe"
outputPath = ".\\packages"
for root, dirNames, filenames in walklevel(".\\", level = 2):
    for filename in fnmatch.filter(filenames, "packages.config"):
        matches.append(os.path.join(root, filename))

for package in matches:
    os.system(nugetPath + " install " + package + " -OutputDirectory " + outputPath)