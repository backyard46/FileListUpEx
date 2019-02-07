
''' <summary>
''' �N���������̒��o�ƕێ����s���N���X�B
''' </summary>
''' <history>
''' [backyarD]	2007/10/02	Created
''' </history>
Public Class ParameterInfo

    ''' <summary>
    ''' �N������������z���n���ƁA���̓��e���v���p�e�B�ɓ]�L���郁�\�b�h�B
    ''' </summary>
    ''' <param name="cmdArgs">cmdArgs</param>
    ''' <exception cref="ApplicationException">���� " & cmdArgs(idx) & " �͈����Ƃ��ĕs�K�؂ł��B</exception>
    ''' <history>
    ''' [backyarD]	2007/10/02	Created
    ''' [backyarD]  2008/04/02  �t�@�C��������p�����Ɋւ��鏈���������B
    ''' </history>
    Public Sub ExtractParameters(ByVal cmdArgs() As String)

        If cmdArgs.Length > 0 Then
            '������1���ȏ゠��ꍇ

            Dim targetParam As String = String.Empty
            For idx As Integer = 0 To cmdArgs.Length - 1
                targetParam = cmdArgs(idx).ToLower

                If targetParam.StartsWith("/folder:") Then
                    _FolderName = targetParam.Substring(8)
                ElseIf targetParam.StartsWith("/result:") Then
                    _ResultFileName = targetParam.Substring(8)
                Else
                    Throw New ApplicationException("���� " & cmdArgs(idx) & " �͈����Ƃ��ĕs�K�؂ł��B")
                End If
            Next
        End If

    End Sub


    ''' <summary>
    ''' �v���p�e�B��String.Empty�ŏ��������郁�\�b�h�B
    ''' </summary>
    ''' <history>
    ''' [backyarD]	2007/10/02	Created
    ''' [backyarD]  2008/04/02  �Ώۃt�@�C�����Ɋւ���ϐ�(_FileName)�̏����������������B
    ''' </history>
    Public Sub Clear()

        _FolderName = String.Empty
        _ResultFileName = String.Empty

    End Sub


    Private _FolderName As String = String.Empty

    ''' <summary>
    ''' �����Ώۃ��[�g�t�H���_��������v���p�e�B�B
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
    ''' ���X�g�A�b�v���ʏo�̓t�@�C����������v���p�e�B�B
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
