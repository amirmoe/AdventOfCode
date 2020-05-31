MIN = 236491
MAX = 713787

def check_adjacent(number: list):
    for i in range(len(number)-1):
        if(number[i] == number[i+1]):
            return True
    return False

def check_increasing(number: list):
    increasing = True
    for i in range(len(number)-1):
        if(number[i] > number[i+1]):
            increasing = False
    return increasing

count = 0
for i in range(MIN+1,MAX):
    if check_adjacent([int(i) for i in str(i)]) and check_increasing([int(i) for i in str(i)]):
        count+=1

print(count)