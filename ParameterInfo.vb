
''' <summary>
''' 起動引数情報の抽出と保持を行うクラス。
''' </summary>
''' <history>
''' [backyarD]	2007/10/02	Created
''' </history>
Public Class ParameterInfo

    ''' <summary>
    ''' 起動引数文字列配列を渡すと、その内容をプロパティに転記するメソッド。
    ''' </summary>
    ''' <param name="cmdArgs">cmdArgs</param>
    ''' <exception cref="ApplicationException">引数 " & cmdArgs(idx) & " は引数として不適切です。</exception>
    ''' <history>
    ''' [backyarD]	2007/10/02	Created
    ''' [backyarD]  2008/04/02  ファイル名特定用引数に関する処理を除去。
    ''' </history>
    Public Sub ExtractParameters(ByVal cmdArgs() As String)

        If cmdArgs.Length > 0 Then
            '引数が1件以上ある場合

            Dim targetParam As String = String.Empty
            For idx As Integer = 0 To cmdArgs.Length - 1
                targetParam = cmdArgs(idx).ToLower

                If targetParam.StartsWith("/folder:") Then
                    _FolderName = targetParam.Substring(8)
                ElseIf targetParam.StartsWith("/result:") Then
                    _ResultFileName = targetParam.Substring(8)
                Else
                    Throw New ApplicationException("引数 " & cmdArgs(idx) & " は引数として不適切です。")
                End If
            Next
        End If

    End Sub


    ''' <summary>
    ''' プロパティをString.Emptyで初期化するメソッド。
    ''' </summary>
    ''' <history>
    ''' [backyarD]	2007/10/02	Created
    ''' [backyarD]  2008/04/02  対象ファイル名に関する変数(_FileName)の初期化処理を除去。
    ''' </history>
    Public Sub Clear()

        _FolderName = String.Empty
        _ResultFileName = String.Empty

    End Sub


    Private _FolderName As String = String.Empty

    ''' <summary>
    ''' 処理対象ルートフォルダ名文字列プロパティ。
    ''' </summary>
    ''' <history>
    ''' [backyarD]	2007/10/02	Created
    ''' </history>
    Public Property FolderName() As String
        Get
            Return _FolderName
        End Get
        Set(ByVal Value As String)
            _FolderName = Value.ToLower
        End Set
    End Property


    Private _ResultFileName As String = String.Empty

    ''' <summary>
    ''' リストアップ結果出力ファイル名文字列プロパティ。
    ''' </summary>
    ''' <history>
    ''' [backyarD]	2007/10/02	Created
    ''' </history>
    Public Property ResultFileName() As String
        Get
            Return _ResultFileName
        End Get
        Set(ByVal Value As String)
            _ResultFileName = Value
        End Set
    End Property

End Class
