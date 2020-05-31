from itertools import groupby

MIN = 236491
MAX = 713787

def check_double(number: list):
    frequency = [len(list(group)) for key, group in groupby(number)]
    return 2 in frequency

def check_increasing(number: list):
    increasing = True
    for i in range(len(number)-1):
        if(number[i] > number[i+1]):
            increasing = False
    return increasing


count = 0
for i in range(MIN+1,MAX):
    if check_double([int(i) for i in str(i)]) and check_increasing([int(i) for i in str(i)]):
        count+=1

print(count)