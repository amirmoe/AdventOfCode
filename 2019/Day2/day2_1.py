import sys

def restore_to_before(array: list):
  array[1] = 12
  array[2] = 2
  return array

def main():
  inp = sys.stdin.readline()
  inp = inp.strip()
  string_list = inp.split(',')
  array = list(map(int, string_list))
  array = restore_to_before(array)
  
  pointer = 0
  while array[pointer] != 99:
    operation = array[pointer]
    if operation == 1:
      array[array[pointer+3]] = array[array[pointer+1]] + array[array[pointer+2]]
    elif operation == 2:
      array[array[pointer+3]] = array[array[pointer+1]] * array[array[pointer+2]]
    else:
      print("Something went wrong!")
    
    pointer+=4
  
  print(array[0])
  

main()


