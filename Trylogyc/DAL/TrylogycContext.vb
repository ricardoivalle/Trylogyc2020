﻿Imports System.Object
Imports System.Web
Imports System.Data.Entity
Imports System.Data.Entity.DbContext
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports Trylogyc.Models
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Data
Imports System.IO
Imports System.Linq
Imports Microsoft.Web

Namespace DAL
    Public Class TrylogycContext
        Inherits DbContext

        Private response As Object
        Public Property ClientScript As Object

        Public Function GetUsuario(ByVal IDUsuario As Int32) As DataSet

            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("TrylogycContext").ToString)
            Dim command As New SqlCommand("", con)
            Dim dA As New SqlDataAdapter()
            Dim ds As New DataSet()
            Dim i As Integer = 0
            Dim sql As String = Nothing
            Dim Prmparametro As New SqlParameter

            With Prmparametro
                .ParameterName = "IDUSUARIO"
                .SqlDbType = Data.SqlDbType.Int
                .Value = IDUsuario
            End With

            With command
                .CommandType = CommandType.StoredProcedure
                .CommandText = "SEL_USUARIO"
                .Parameters.Add(Prmparametro)
            End With

            Try
                con.Open()
                dA.SelectCommand = command
                dA.Fill(ds)
                dA.Dispose()
                command.Dispose()
                con.Close()
                Return ds
            Catch ex As Exception
                ds.Tables.Add()
                ds.Tables(0).TableName = "Error"
                ds.Tables(0).Rows.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Rows(0).Item(0) = ex.HResult
                ds.Tables(0).Rows(0).Item(1) = ex.Message
                Return ds
            End Try
        End Function
        Public Function GetUsuarioNombre(ByVal userName As String) As DataSet
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("TrylogycContext").ToString)
            Dim command As New SqlCommand("", con)
            Dim dA As New SqlDataAdapter()
            Dim ds As New DataSet()
            Dim i As Integer = 0
            Dim sql As String = Nothing
            Dim Prmparametro As New SqlParameter

            With Prmparametro
                .ParameterName = "USERNAME"
                .SqlDbType = Data.SqlDbType.VarChar
                .Value = userName
            End With

            With command
                .CommandType = CommandType.StoredProcedure
                .CommandText = "SEL_USUARIO_CGP"
                .Parameters.Add(Prmparametro)
            End With

            Try
                con.Open()
                dA.SelectCommand = command
                dA.Fill(ds)
                dA.Dispose()
                command.Dispose()
                con.Close()

            Catch ex As Exception
                ds.Tables.Add()
                ds.Tables(0).TableName = "Error"
                ds.Tables(0).Rows.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Rows(0).Item(0) = ex.HResult
                ds.Tables(0).Rows(0).Item(1) = ex.Message
                Return ds
            End Try
            Return ds
        End Function
        Public Function GetSocio(ByVal codSocio As Int32) As DataSet
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("TrylogycContext").ToString)
            Dim command As New SqlCommand("", con)
            Dim dA As New SqlDataAdapter()
            Dim ds As New DataSet()
            Dim i As Integer = 0
            Dim sql As String = Nothing
            Dim Prmparametro As New SqlParameter

            With Prmparametro
                .ParameterName = "SOCCGP"
                .SqlDbType = Data.SqlDbType.Int
                .Value = codSocio
            End With

            With command
                .CommandType = CommandType.StoredProcedure
                .CommandText = "SEL_CNT_SOCIOS_CGP"
                .Parameters.Add(Prmparametro)
            End With

            Try
                con.Open()
                dA.SelectCommand = command
                dA.Fill(ds)
                dA.Dispose()
                command.Dispose()
                con.Close()
            Catch ex As Exception
                ds.Tables.Add()
                ds.Tables(0).TableName = "Error"
                ds.Tables(0).Rows.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Rows(0).Item(0) = ex.HResult
                ds.Tables(0).Rows(0).Item(1) = ex.Message
                Return ds

            End Try

        End Function
        Public Function PutRelacion(ByVal idUsuario As Int32, ByVal idSocio As Int32, ByVal idConexion As Int32) As DataSet
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("TrylogycContext").ToString)
            Dim command As New SqlCommand("", con)
            Dim dA As New SqlDataAdapter()
            Dim ds As New DataSet()
            Dim i As Integer = 0
            Dim sql As String = Nothing
            Dim Prmparametro1 As New SqlParameter
            Dim Prmparametro2 As New SqlParameter
            Dim Prmparametro3 As New SqlParameter
            With Prmparametro1
                .ParameterName = "idUsuario"
                .SqlDbType = Data.SqlDbType.Int
                .Value = idUsuario
            End With
            With Prmparametro2
                .ParameterName = "idSocio"
                .SqlDbType = Data.SqlDbType.Int
                .Value = idSocio
            End With
            With Prmparametro3
                .ParameterName = "idConexion"
                .SqlDbType = Data.SqlDbType.Int
                .Value = idConexion
            End With
            With command
                .CommandType = CommandType.StoredProcedure
                .CommandText = "INS_SOCIOS_CONEXIONES"
                .Parameters.Add(Prmparametro1)
                .Parameters.Add(Prmparametro2)
                .Parameters.Add(Prmparametro3)
            End With

            Try
                con.Open()
                dA.SelectCommand = command
                dA.Fill(ds)
                dA.Dispose()
                command.Dispose()
                con.Close()

                Return ds
            Catch ex As Exception
                ds.Tables.Add()
                ds.Tables(0).TableName = "Error"
                ds.Tables(0).Rows.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Rows(0).Item(0) = ex.HResult
                ds.Tables(0).Rows(0).Item(1) = ex.Message
                Return ds
            End Try
        End Function

        Public Function GetSocios() As DataSet
            Dim dataSet1 As DataSet = New DataSet
            Try

                Dim ficheroXML As String = HttpContext.Current.Server.MapPath("~\xmlfiles\SOCIOS.xml")
                dataSet1.ReadXml(ficheroXML, XmlReadMode.InferSchema)
                Return dataSet1
            Catch ex As Exception
                dataSet1.Tables.Add()
                dataSet1.Tables(0).TableName = "Error"
                dataSet1.Tables(0).Rows.Add()
                dataSet1.Tables(0).Columns.Add()
                dataSet1.Tables(0).Columns.Add()
                dataSet1.Tables(0).Rows(0).Item(0) = ex.HResult
                dataSet1.Tables(0).Rows(0).Item(1) = ex.Message
                Return dataSet1
            End Try
        End Function

        Public Function GetUsuarioSocio(ByVal codSocio As Int32, email As String) As Int32
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("TrylogycContext").ToString)
            Dim command As New SqlCommand("", con)
            Dim dA As New SqlDataAdapter()
            Dim ds As New DataSet()
            Dim i As Integer = 0
            Dim sql As String = Nothing
            Dim Prmparametro1 As New SqlParameter
            Dim Prmparametro2 As New SqlParameter
            Dim retCount As Int32
            With Prmparametro1
                .ParameterName = "IDSOCIO"
                .SqlDbType = Data.SqlDbType.Int
                .Value = -1
            End With
            With Prmparametro2
                .ParameterName = "EMAIL"
                .SqlDbType = Data.SqlDbType.VarChar
                .Value = email
            End With
            With command
                .CommandType = CommandType.StoredProcedure
                .CommandText = "SEL_USUARIO_IDSOCIO_EMAIL"
                .Parameters.Add(Prmparametro1)
                .Parameters.Add(Prmparametro2)
            End With

            Try
                con.Open()
                dA.SelectCommand = command
                dA.Fill(ds)
                dA.Dispose()
                retCount = ds.Tables(0).Rows(0).Item(0)
                Return retCount
                command.Dispose()
                con.Close()
                Return retCount
            Catch ex As Exception

                Return 0
            End Try

        End Function
        Public Function GetUsuarioReestablecer(ByVal codSocio As Int32, ByVal email As String) As DataSet
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("TrylogycContext").ToString)
            Dim command As New SqlCommand("", con)
            Dim dA As New SqlDataAdapter()
            Dim ds As New DataSet()
            Dim i As Integer = 0
            Dim sql As String = Nothing
            Dim Prmparametro1 As New SqlParameter
            Dim Prmparametro2 As New SqlParameter
            With Prmparametro1
                .ParameterName = "XmlSocio"
                .SqlDbType = Data.SqlDbType.Int
                .Value = codSocio
            End With
            With Prmparametro2
                .ParameterName = "userEmail"
                .SqlDbType = Data.SqlDbType.VarChar
                .Value = email
            End With
            With command
                .CommandType = CommandType.StoredProcedure
                .CommandText = "SEL_USUARIO_REESTABLECER"
                .Parameters.Add(Prmparametro1)
                .Parameters.Add(Prmparametro2)
            End With

            Try
                con.Open()
                dA.SelectCommand = command
                dA.Fill(ds)
                dA.Dispose()
                command.Dispose()

                Return ds
            Catch ex As Exception
                ds.Tables.Add()
                ds.Tables(0).TableName = "Error"
                ds.Tables(0).Rows.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Rows(0).Item(0) = ex.HResult
                ds.Tables(0).Rows(0).Item(1) = ex.Message
                Return ds

            End Try
            dA.Dispose()
            con.Close()
        End Function

        Public Function PutUsuario(ByVal email As String, ByVal codSocio As Int32, ByVal idConexion As Int32) As DataSet
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("TrylogycContext").ToString)
            Dim command As New SqlCommand("", con)
            Dim dA As New SqlDataAdapter()
            Dim ds As New DataSet()
            Dim i As Integer = 0
            Dim sql As String = Nothing
            Dim Prmparametro1 As New SqlParameter
            Dim Prmparametro2 As New SqlParameter
            Dim Prmparametro3 As New SqlParameter
            With Prmparametro1
                .ParameterName = "email"
                .SqlDbType = Data.SqlDbType.VarChar
                .Value = email
            End With
            With Prmparametro2
                .ParameterName = "XmlSocio"
                .SqlDbType = Data.SqlDbType.Int
                .Value = codSocio
            End With
            With Prmparametro3
                .ParameterName = "idConexion"
                .SqlDbType = Data.SqlDbType.Int
                .Value = idConexion
            End With
            With command
                .CommandType = CommandType.StoredProcedure
                .CommandText = "INS_USUARIO"
                .Parameters.Add(Prmparametro1)
                .Parameters.Add(Prmparametro2)
                .Parameters.Add(Prmparametro3)
            End With

            Try
                con.Open()
                dA.SelectCommand = command
                dA.Fill(ds)
                dA.Dispose()
                command.Dispose()
                con.Close()
                Return ds
            Catch ex As Exception
                ds.Tables.Add()
                ds.Tables(0).TableName = "Error"
                ds.Tables(0).Rows.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Rows(0).Item(0) = ex.HResult
                ds.Tables(0).Rows(0).Item(1) = ex.Message
                Return ds
            End Try
        End Function
        Public Function GetSocioConexion(ByVal idUsuario As Int32) As DataSet
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("TrylogycContext").ToString)
            Dim command As New SqlCommand("", con)
            Dim dA As New SqlDataAdapter()
            Dim ds As New DataSet()
            Dim i As Integer = 0
            Dim sql As String = Nothing
            Dim Prmparametro1 As New SqlParameter
            With Prmparametro1
                .ParameterName = "idUsuario"
                .SqlDbType = Data.SqlDbType.VarChar
                .Value = idUsuario
            End With
            With command
                .CommandType = CommandType.StoredProcedure
                .CommandText = "SEL_SOCIOCONEXION"
                .Parameters.Add(Prmparametro1)
            End With

            Try
                con.Open()
                dA.SelectCommand = command
                dA.Fill(ds)
                dA.Dispose()
                command.Dispose()
                con.Close()
                Return ds
            Catch ex As Exception
                ds.Tables.Add()
                ds.Tables(0).TableName = "Error"
                ds.Tables(0).Rows.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Rows(0).Item(0) = ex.HResult
                ds.Tables(0).Rows(0).Item(1) = ex.Message
                Return ds
            End Try

        End Function
        Public Function GetConexiones(ByVal XmlSocio As Int32) As DataSet
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("TrylogycContext").ToString)
            Dim command As New SqlCommand("", con)
            Dim dA As New SqlDataAdapter()
            Dim ds As New DataSet()
            Dim i As Integer = 0
            Dim sql As String = Nothing
            Dim Prmparametro As New SqlParameter

            With Prmparametro
                .ParameterName = "XmlSocio"
                .SqlDbType = Data.SqlDbType.Int
                .Value = XmlSocio
            End With

            With command
                .CommandType = CommandType.StoredProcedure
                .CommandText = "SEL_CONEXIONES_SOCIO"
                .Parameters.Add(Prmparametro)
            End With

            Try
                con.Open()
                dA.SelectCommand = command
                dA.Fill(ds)
                dA.Dispose()
                command.Dispose()
                con.Close()
                Return ds
            Catch ex As Exception
                ds.Tables.Add()
                ds.Tables(0).TableName = "Error"
                ds.Tables(0).Rows.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Rows(0).Item(0) = ex.HResult
                ds.Tables(0).Rows(0).Item(1) = ex.Message
                Return ds
            End Try
        End Function
        Public Function GetCops(ByVal idUsuario As Int32) As DataSet

            Dim ds As New DataSet()
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("TrylogycContext").ToString)
            Dim command As New SqlCommand("", con)
            Dim dA As New SqlDataAdapter()
            Dim i As Integer = 0
            Dim sql As String = Nothing
            Dim Prmparametro As New SqlParameter

            With Prmparametro
                .ParameterName = "idUsuario"
                .SqlDbType = Data.SqlDbType.Int
                .Value = idUsuario
            End With

            With command
                .CommandType = CommandType.StoredProcedure
                .CommandText = "SEL_SOCIOCONEXION_CNT"
                .Parameters.Add(Prmparametro)
            End With

            Try
                con.Open()
                dA.SelectCommand = command
                dA.Fill(ds)
                dA.Dispose()
                command.Dispose()
                con.Close()
                Return ds
            Catch ex As Exception
                ds.Tables.Add()
                ds.Tables(0).TableName = "Error"
                ds.Tables(0).Rows.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Rows(0).Item(0) = ex.HResult
                ds.Tables(0).Rows(0).Item(1) = ex.Message
                Return ds
            End Try
            '    xmlFile = XmlReader.Create(HttpContext.Current.Server.MapPath("/xmlfiles/SALDOS.xml"), New XmlReaderSettings())
            '    ds.ReadXml(xmlFile)
            '    Dim facturas As DataTable = ds.Tables(0)
            '    Dim query =
            '     (From factura In facturas.AsEnumerable()
            '      Where factura.Item(0) = xmlSocio.ToString).Count

            '    Dim dsFacturas As New DataSet
            '    dsFacturas.Tables.Add()
            '    For t = 0 To 1
            '        dsFacturas.Tables(0).Columns.Add()
            '    Next
            '    dsFacturas.Tables(0).Rows.Add()
            '    dsFacturas.Tables(0).Rows(0).Item(0) = "Facturas"
            '    dsFacturas.Tables(0).Rows(0).Item(1) = query
            '    Return dsFacturas
            'Catch ex As Exception
            '    ds.Tables.Add()
            '    ds.Tables(0).TableName = "Error"
            '    ds.Tables(0).Rows.Add()
            '    ds.Tables(0).Columns.Add()
            '    ds.Tables(0).Columns.Add()
            '    ds.Tables(0).Rows(0).Item(0) = ex.HResult
            '    ds.Tables(0).Rows(0).Item(1) = ex.Message
            '    Return ds
        End Function
        Public Function GetConexionDetalles(ByVal XmlConexion As Int32) As DataSet
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("TrylogycContext").ToString)
            Dim command As New SqlCommand("", con)
            Dim dA As New SqlDataAdapter()
            Dim ds As New DataSet()
            Dim i As Integer = 0
            Dim sql As String = Nothing
            Dim Prmparametro As New SqlParameter

            With Prmparametro
                .ParameterName = "XmlConexion"
                .SqlDbType = Data.SqlDbType.Int
                .Value = XmlConexion
            End With

            With command
                .CommandType = CommandType.StoredProcedure
                .CommandText = "SEL_CONEXION_DETALLES"
                .Parameters.Add(Prmparametro)
            End With

            Try
                con.Open()
                dA.SelectCommand = command
                dA.Fill(ds)
                dA.Dispose()
                command.Dispose()
                con.Close()
                Return ds
            Catch ex As Exception
                ds.Tables.Add()
                ds.Tables(0).TableName = "Error"
                ds.Tables(0).Rows.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Rows(0).Item(0) = ex.HResult
                ds.Tables(0).Rows(0).Item(1) = ex.Message
                Return ds
            End Try
        End Function
        Public Function UpdUsuario(ByVal idUsuario As Int32, ByVal passWord As String) As DataSet
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("TrylogycContext").ToString)
            Dim command As New SqlCommand("", con)
            Dim dA As New SqlDataAdapter()
            Dim ds As New DataSet()
            Dim i As Integer = 0
            Dim sql As String = Nothing
            Dim Prmparametro1 As New SqlParameter
            Dim Prmparametro2 As New SqlParameter

            With Prmparametro1
                .ParameterName = "idUsuario"
                .SqlDbType = Data.SqlDbType.Int
                .Value = idUsuario
            End With
            With Prmparametro2
                .ParameterName = "passWord"
                .SqlDbType = Data.SqlDbType.VarChar
                .Value = passWord
            End With
            With command
                .CommandType = CommandType.StoredProcedure
                .CommandText = "UPD_USUARIO_CONTRASENA"
                .Parameters.Add(Prmparametro1)
                .Parameters.Add(Prmparametro2)
            End With

            Try
                con.Open()
                dA.SelectCommand = command
                dA.Fill(ds)
                dA.Dispose()
                command.Dispose()
                con.Close()
                Return ds
            Catch ex As Exception
                ds.Tables.Add()
                ds.Tables(0).TableName = "Error"
                ds.Tables(0).Rows.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Columns.Add()
                ds.Tables(0).Rows(0).Item(0) = ex.HResult
                ds.Tables(0).Rows(0).Item(1) = ex.Message
                Return ds
            End Try
        End Function
    End Class
End Namespace