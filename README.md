# Clínica San Miguel - Sistema de Citas Médicas

Sistema web para reservar citas médicas online desarrollado en ASP.NET Core 8.0.

## Características

- **Registro y Login de pacientes**
- **Gestión de familiares dependientes** 
- **Reserva de citas médicas paso a paso**
- **Dashboard personalizado con historial**
- **Integración con seguros de salud**
- **Múltiples clínicas y especialidades**

## Tecnologías

- **ASP.NET Core 8.0** - Framework web
- **C#** - Lenguaje backend
- **SQL Server** - Base de datos
- **Bootstrap 5** - UI responsive
- **jQuery** - Interactividad frontend

## Requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads) o superior
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recomendado)

## Instalación

### 1. Base de Datos
1. Abrir **SQL Server Management Studio**
2. Ejecutar estos scripts en orden:
   ```
   db/DBSanMiguel.sql                 (estructura)
   db/DBSanMiguel_Inserts.sql         (datos básicos)
   db/DBSanMiguel_StoreProcedures.sql (procedimientos)
   db/DBSanMiguel_Tests.sql           (datos de prueba)
   ```

### 2. Configuración
Editar `appsettings.json` con tu servidor SQL:
```json
{
  "ConnectionStrings": {
    "stringConexion": "server=.\\SQLEXPRESS;database=DBSanMiguel;Trusted_connection=true;MultipleActiveResultSets=true;TrustServerCertificate=false;Encrypt=false"
  }
}
```

### 3. Ejecutar
```bash
dotnet restore
dotnet build
dotnet run
```

La aplicación estará disponible en: **https://localhost:7231**

## Usuario de Prueba

- **Tipo Documento**: DNI
- **Documento**: `11223344`
- **Contraseña**: `pass123`

## Estructura del Proyecto

```
ClinicaSanMiguel/
├── Controllers/        # Controladores MVC
├── Models/            # Modelos de base de datos
├── Views/             # Vistas Razor
├── DTOs/              # Objetos de transferencia
├── Repositorys/       # Patrón Repository
├── wwwroot/           # Archivos estáticos
└── db/                # Scripts de base de datos
```

## 📱 Funcionalidades

### Dashboard del Paciente
- Perfil médico completo (peso, altura, tipo sangre)
- Citas programadas y historial
- Tips de salud
- Gestión de familiares

### Reserva de Citas (4 Pasos)
1. **Seleccionar paciente** (titular o familiar)
2. **Elegir especialidad** y clínica
3. **Seleccionar médico** y fecha/hora
4. **Confirmar** con seguro y precio final

### Gestión de Familiares
- Agregar dependientes con parentesco
- Reservar citas para familiares
- Historial conjunto de citas

## Base de Datos

### Tablas principales:
- `Pacientes` - Información personal y médica
- `CitaMedica` - Citas reservadas
- `Medicos` - Médicos y especialidades
- `Clinicas` - Sedes médicas
- `SeguroSalud` - Seguros con coberturas

### Stored Procedures clave:
- `InicioSesionSP` - Login de pacientes
- `ReservarCitaMedicaSP` - Crear citas
- `CitasPacienteActivasSP` - Citas programadas
- `HistorialCitasPacienteSP` - Historial médico

## Problemas Comunes

**Error de conexión DB:**
- Verificar que SQL Server esté corriendo
- Confirmar cadena de conexión
- Ejecutar scripts de base de datos

**Puerto ocupado:**
```bash
dotnet run --urls="https://localhost:8001"
```

## Despliegue

### IIS (Windows)
```bash
dotnet publish -c Release -o ./publish
```

### Docker
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY publish/ .
ENTRYPOINT ["dotnet", "ClinicaSanMiguel.dll"]
```

## Datos de Prueba Incluidos

- **3 pacientes** con familiares
- **15 especialidades** médicas  
- **70+ médicos** en todas las especialidades
- **3 clínicas** (Principal, Norte, Sur)
- **10 seguros** con diferentes coberturas

## Desarrollo

Estructura del código:
- **Patrón MVC** para separación de responsabilidades
- **Repository Pattern** para acceso a datos
- **DTOs** para transferencia segura
- **Stored Procedures** para lógica de BD
- **Bootstrap** para UI responsive

## Licencia

Proyecto académico bajo licencia MIT.

---

*Desarrollado como proyecto académico para Desarrollo de Software Web*