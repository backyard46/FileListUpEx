
Imports System.IO
Imports System.Text

''' <summary>
''' �又���B
''' </summary>
''' <history>
''' [backyarD]	2007/10/02	Created
''' [backyarD]  2008/04/02  MakeFullPathList���x�[�X�ɁA�t�@�C�����Ɋւ������
''' �������A�S�t�@�C�����擾�A�v���ɉ����B
''' </history>
Public Class MainClass


    ''' <summary>
    ''' �又��
    ''' </summary>
    ''' <param name="CmdArgs">CmdArgs</param>
    ''' <history>
    ''' [backyarD]	2007/10/02	Created
    ''' [backyarD]    2008/04/02  �Ώۃt�@�C�����Ɋւ���w�菈���������B
    ''' </history>
    Public Shared Sub Main(ByVal CmdArgs() As String)

        Try
            Dim parameters As New ParameterInfo
            parameters.ExtractParameters(CmdArgs)

            If IO.Directory.Exists(parameters.FolderName) Then
                '�t�H���_�����݂���ꍇ
                Dim targetDir As New DirectoryInfo(parameters.FolderName)
                Dim fileList As ArrayList = ListUpFileNames(targetDir)

                WriteFileList(fileList, parameters.ResultFileName)

            Else
                '�t�H���_�����݂��Ȃ��ꍇ���G���[�Ƃ͂��Ȃ����A�I���
                Console.WriteLine("[�x��] �w�肳�ꂽ�t�H���_�u" & parameters.FolderName & "�v�͑��݂��܂���B")
            End If

        Catch ex As Exception
            '�G���[������
            Console.WriteLine("[�G���[] �t�@�C�����X�g�쐬�����ŃG���[���������܂����B")
            Console.WriteLine(ex.Message)

        End Try


    End Sub


    ''' <summary>
    ''' �t�@�C�������[�߂�ꂽArrayList�̓��e���A�w�肵���t�@�C���ɏ����o���B
    ''' </summary>
    ''' <param name="FileList">�t�@�C���������񂪊i�[���ꂽArrayList�B</param>
    ''' <param name="ListFileName">�����o����t�@�C����</param>
    ''' <history>
    ''' [backyarD]	2007/10/02	Created
    ''' </history>
    Private Shared Sub WriteFileList(ByVal FileList As ArrayList, ByVal ListFileName As String)

        Dim writeStream As New FileStream(ListFileName, FileMode.Create)
        Dim writer As New StreamWriter(writeStream, Encoding.GetEncoding(932))

        Dim builder As New StringBuilder
        writer.WriteLine("�t�@�C����,�T�C�Y,�ύX��")
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
    ''' �w�肳�ꂽ�t�H���_�ɂ���t�@�C�����ꗗ��ArrayList�ŕԂ��B
    ''' </summary>
    ''' <param name="TargetDirectory">TargetDirectory</param>
    ''' <returns></returns>
    ''' <history>
    ''' [backyarD]	2007/08/16	Created
    ''' [backyarD]    2007/10/02  FilePropertyChecker�̃\�[�X����ڐA�B
    ''' [backyarD]    2008/04/02  �S�t�@�C���Ώۃo�[�W�����ւ̉����ɔ����A�Ώۃt�@�C����
    ''' �Ɋւ�������������B
    ''' </history>
    Private Shared Function ListUpFileNames(ByVal TargetDirectory As DirectoryInfo) As ArrayList

        Dim resultArray As New ArrayList

        '�܂��͂��̃t�H���_�̃t�@�C�������擾
        resultArray = ListUpFileInDir(TargetDirectory)

        '���Ƀt�H���_�ꗗ���擾
        '�ċA�Ăяo���Ō@���Ă���
        Dim targetDirs() As DirectoryInfo = TargetDirectory.GetDirectories
        For Each targetDir As DirectoryInfo In targetDirs
            resultArray.AddRange(ListUpFileNames(targetDir))
        Next

        Return resultArray

    End Function


    ''' <summary>
    ''' �w�肳�ꂽDirectoryInfo�Ɋ܂܂��t�@�C�����ꗗ���擾����
    ''' </summary>
    ''' <param name="TargetDirectory">TargetDirectory</param>
    ''' <returns></returns>
    ''' <history>
    ''' [backyarD]	2007/08/16	Created
    ''' [backyarD]    2007/10/02  FilePropertyChecker�̃\�[�X����ڐA�B
    ''' �g���q�w�����菜���B����t�@�C�����̃t���p�X�������o����悤�����B
    ''' [backyarD]    2008/04/02  ����ɑS�t�@�C���擾�o�[�W�����ɉ����B�t�@�C���������A�����������B
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
