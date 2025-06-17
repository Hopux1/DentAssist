
# 🦷 DentAssist – Sistema de Gestión Odontológica

**DentAssist** es una aplicación web ASP.NET Core MVC diseñada para la gestión integral de pacientes, odontólogos, turnos y planes de tratamiento en clínicas dentales. Permite registrar, visualizar y controlar todas las atenciones odontológicas desde un panel unificado y fácil de usar.

---

## 📋 Características Principales

- Registro y gestión de **pacientes** (con validación de RUT y formato automático).
- Gestión de **odontólogos** con matrícula profesional.
- Administración de **turnos** (evitando duplicados o fechas pasadas).
- Registro de **planes de tratamiento** por paciente y odontólogo.
- Creación de **pasos de tratamiento** controlados por fecha, estado y observaciones.
- Exportación de planes en formato **PDF profesional**, con logo, firma y estilo clínico.
- Filtros por nombre del paciente en turnos y planes de tratamiento.
- Validaciones de integridad y fechas en formularios.
- Compatible con SQL Server LocalDB para fácil ejecución local.

---

## 🚀 Instalación y Configuración

### 1. Requisitos
- Visual Studio 2022 o superior
- .NET 6 SDK
- SQL Server LocalDB (incluido en Visual Studio)

### 2. Clona el repositorio
```bash
git clone https://github.com/usuario/DentAssist.git
```

### 3. Configura la base de datos
La app ya está configurada para usar LocalDB:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DentAssistDb;Trusted_Connection=True;"
}
```

Si lo necesitas cambiar:
- Abre `appsettings.json` y ajusta la cadena a tu servidor.

### 4. Ejecuta la aplicación
- Abre Visual Studio.
- Ejecuta `Update-Database` desde **Package Manager Console** para crear la base de datos.
- Presiona F5 o `Ctrl+F5` para iniciar el proyecto.

---

## 👨‍⚕️ Manual de Usuario

### ▶️ Iniciar sesión
No es necesario iniciar sesión para usar el sistema.

### 🧑‍💼 Pacientes
- Ir a `Pacientes > Crear` para agregar un nuevo paciente.
- El RUT se valida automáticamente y se formatea con puntos y guion.
- No se permite duplicar un RUT.

### 📅 Turnos
- Puedes asignar turnos a pacientes desde `Turnos > Crear`.
- No se permiten fechas en el pasado.
- Filtra por nombre del paciente en la vista `Index`.

### 🦷 Planes de Tratamiento
- Ir a `Planes de Tratamiento > Crear` para generar un nuevo plan.
- Cada plan se asocia a un paciente y a un odontólogo.
- No se puede crear un plan nuevo si ya existe uno activo con el mismo odontólogo.

### 🧾 Pasos del Tratamiento
- Se agregan desde los detalles del plan.
- Cada paso tiene un tratamiento, una fecha estimada y un estado.
- No se permite agregar pasos a planes finalizados.
- No se permiten fechas pasadas.

### 🧾 Exportar a PDF
- Desde `Planes de Tratamiento > Detalles`, puedes generar un PDF profesional:
  - Incluye logo de la clínica (`logo.png`)
  - Firma, fecha y pie de página institucional
  - Estilo con bordes y colores médicos

---

## 🎨 Personalización

### Cambiar logo
Reemplaza el archivo `wwwroot/logo.png` con el de tu clínica.

### Favicon
Agrega tu ícono en `wwwroot/favicon.ico` y asegúrate de tenerlo en `_Layout.cshtml`:
```html
<link rel="icon" type="image/x-icon" href="~/favicon.ico" />
```

---

## 👥 Autores
- 👨‍💻 Desarrollador principal: *[Tu nombre]*

---

## 🛠️ Tecnologías
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server LocalDB
- Bootstrap 5
- Rotativa.AspNetCore (PDF)

---

## 📄 Licencia
Este proyecto es de uso académico y puede ser adaptado libremente con fines educativos.

---
