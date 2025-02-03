import os
import chardet

def count_cs_lines(directory):
    """
    计算指定目录及其子目录中所有 C# 代码文件的行数。

    Args:
        directory: 要扫描的目录路径。

    Returns:
        总代码行数。
    """
    total_lines = 0
    for root, _, files in os.walk(directory):
        for file in files:
            if file.endswith(".cs"):
                filepath = os.path.join(root, file)
                try:
                    with open(filepath, 'rb') as f:
                        rawdata = f.read()
                        result = chardet.detect(rawdata)
                        encoding = result['encoding']
                    
                    with open(filepath, 'r', encoding=encoding) as f:
                        for line in f:
                            # 去除行首尾的空白字符，并跳过空行和注释行
                            stripped_line = line.strip()
                            if stripped_line and not stripped_line.startswith("//"):
                                total_lines += 1
                except UnicodeDecodeError:
                    print(f"无法解码文件: {filepath}, 跳过")
                except Exception as e:
                    print(f"读取文件 {filepath} 时发生错误: {e}")
                    continue
    return total_lines

if __name__ == "__main__":
    target_directory = input("请输入要扫描的目录路径: ")
    if os.path.exists(target_directory) and os.path.isdir(target_directory):
        line_count = count_cs_lines(target_directory)
        print(f"目录 '{target_directory}' 下所有 C# 代码总行数为: {line_count}")
    else:
        print("输入的目录路径无效或不存在。")