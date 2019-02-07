
Imports System.IO
Imports System.Text

''' <summary>
''' 主処理。
''' </summary>
''' <history>
''' [backyarD]	2007/10/02	Created
''' [backyarD]  2008/04/02  MakeFullPathListをベースに、ファイル名に関する条件
''' を除去、全ファイル情報取得アプリに改造。
''' </history>
Public Class MainClass


    ''' <summary>
    ''' 主処理
    ''' </summary>
    ''' <param name="CmdArgs">CmdArgs</param>
    ''' <history>
    ''' [backyarD]	2007/10/02	Created
    ''' [backyarD]    2008/04/02  対象ファイル名に関する指定処理を除去。
    ''' </history>
    Public Shared Sub Main(ByVal CmdArgs() As String)

        Try
            Dim parameters As New ParameterInfo
            parameters.ExtractParameters(CmdArgs)

            If IO.Directory.Exists(parameters.FolderName) Then
                'フォルダが存在する場合
                Dim targetDir As New DirectoryInfo(parameters.FolderName)
                Dim fileList As ArrayList = ListUpFileNames(targetDir)

                WriteFileList(fileList, parameters.ResultFileName)

            Else
                'フォルダが存在しない場合→エラーとはしないが、終わる
                Console.WriteLine("[警告] 指定されたフォルダ「" & parameters.FolderName & "」は存在しません。")
            End If

        Catch ex As Exception
            'エラー発生時
            Console.WriteLine("[エラー] ファイルリスト作成処理でエラーが発生しました。")
            Console.WriteLine(ex.Message)

        End Try


    End Sub


    ''' <summary>
    ''' ファイル名が納められたArrayListの内容を、指定したファイルに書き出す。
    ''' </summary>
    ''' <param name="FileList">ファイル名文字列が格納されたArrayList。</param>
    ''' <param name="ListFileName">書き出し先ファイル名</param>
    ''' <history>
    ''' [backyarD]	2007/10/02	Created
    ''' </history>
    Private Shared Sub WriteFileList(ByVal FileList As ArrayList, ByVal ListFileName As String)

        Dim writeStream As New FileStream(ListFileName, FileMode.Create)
        Dim writer As New StreamWriter(writeStream, Encoding.GetEncoding(932))

        Dim builder As New StringBuilder
        writer.WriteLine("ファイル名,サイズ,変更日")
        For Each fileName As String In FileList
            builder = New StringBuilder
            Dim targetFile As New FileInfo(fileName)
            builder.Append("""")
            builder.Append(targetFile.FullName)
            builder.Append(""",""")
            builder.Append(targetFile.Length.ToString)
            builder.Append(""",""")
            builder.Append(targetFile.LastWriteTime.ToString)
            builder.Append("""")
            writer.WriteLine(builder.ToString)
        Next

        writer.Close()
        writeStream.Close()

    End Sub


    ''' <summary>
    ''' 指定されたフォルダにあるファイル名一覧をArrayListで返す。
    ''' </summary>
    ''' <param name="TargetDirectory">TargetDirectory</param>
    ''' <returns></returns>
    ''' <history>
    ''' [backyarD]	2007/08/16	Created
    ''' [backyarD]    2007/10/02  FilePropertyCheckerのソースから移植。
    ''' [backyarD]    2008/04/02  全ファイル対象バージョンへの改造に伴い、対象ファイル名
    ''' に関する引数を除去。
    ''' </history>
    Private Shared Function ListUpFileNames(ByVal TargetDirectory As DirectoryInfo) As ArrayList

        Dim resultArray As New ArrayList

        'まずはそのフォルダのファイル名を取得
        resultArray = ListUpFileInDir(TargetDirectory)

        '次にフォルダ一覧を取得
        '再帰呼び出しで掘っていく
        Dim targetDirs() As DirectoryInfo = TargetDirectory.GetDirectories
        For Each targetDir As DirectoryInfo In targetDirs
            resultArray.AddRange(ListUpFileNames(targetDir))
        Next

        Return resultArray

    End Function


    ''' <summary>
    ''' 指定されたDirectoryInfoに含まれるファイル名一覧を取得する
    ''' </summary>
    ''' <param name="TargetDirectory">TargetDirectory</param>
    ''' <returns></returns>
    ''' <history>
    ''' [backyarD]	2007/08/16	Created
    ''' [backyarD]    2007/10/02  FilePropertyCheckerのソースから移植。
    ''' 拡張子指定を取り除く。特定ファイル名のフルパスだけ抽出するよう改造。
    ''' [backyarD]    2008/04/02  さらに全ファイル取得バージョンに改造。ファイル名条件、引数を除去。
    ''' </history>
    Private Shared Function ListUpFileInDir(ByVal TargetDirectory As DirectoryInfo) As ArrayList

        Dim result As New ArrayList
        Dim resultFileInfo() As FileInfo

        resultFileInfo = TargetDirectory.GetFiles
        For Each fileObj As FileInfo In resultFileInfo
            result.Add(fileObj.FullName)
        Next

        Return result

    End Function

End Class
