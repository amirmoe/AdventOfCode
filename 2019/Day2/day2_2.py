import sys


def main():
  inp = sys.stdin.readline()
  inp = inp.strip()
  string_list = inp.split(',')
  original_array = list(map(int, string_list))


  for i in range(100):
    for j in range(100):
      array = original_array.copy()

      array[1] = i
      array[2] = j
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

      if array[0] == 19690720:
        print ("yes!")
        print(i)
        print(j)
        return (100*i+j)
  
  

print(main())
  