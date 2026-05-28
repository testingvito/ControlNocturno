# Control Nocturno de Restricción Horaria v1.0 🌙

Aplicación de servicio en segundo plano desarrollada en **Visual Basic .NET (Windows Forms)** diseñada para la gestión de tiempo de uso y apagado automático forzado de equipos Windows bajo rangos horarios específicos. 

El sistema está optimizado para ejecutarse de forma sigilosa y eficiente, ideal para entornos de control o administración de estaciones de trabajo.

---

## ✨ Características Principales

*   **Inicio 100% Invisible:** La aplicación arranca de manera completamente oculta. No genera ventanas emergentes ni parpadeos en el escritorio, protegiendo su presencia.
*   **Gestión en Segundo Plano:** Se aloja directamente en el área de notificación de la barra de tareas (`NotifyIcon`) junto al reloj del sistema.
*   **Control de Instancia Única (`Mutex`):** Bloquea la duplicación de procesos en la memoria RAM. Si se intenta abrir el programa más de una vez, el sistema muestra una advertencia amigable y cierra la nueva copia de forma segura.
*   **Persistencia en el Sistema:** Opción integrada para registrar o remover la aplicación del auto-arranque de Windows (`Registry`).
*   **Configuración Externa Dinámica:** Lee y escribe las preferencias en un archivo independiente `config.ini`, manteniendo la flexibilidad sin necesidad de recompilar el código.

---

## 🛠️ Requisitos del Sistema

*   **Sistema Operativo:** Windows 10 / Windows 11 (X64)
*   **Entorno de Ejecución:** .NET 6.0 o .NET 8.0 Runtime (según la compilación seleccionada)
*   **Permisos:** Se requieren privilegios de administración local para la gestión del auto-arranque en el registro.

---

## ⚙️ Guía de Uso y Configuración

### 1. Primer Arranque
Al ejecutar la aplicación por primera vez, esta nacerá desactivada por seguridad y creará automáticamente un archivo llamado `config.ini` en el mismo directorio del ejecutable.

### 2. Modificación de Horarios
Para establecer las horas en las que el equipo **no tiene autorización** para estar encendido, abre el archivo `config.ini` con el Bloc de Notas y edita los siguientes parámetros:

```ini
[Configuracion]
HoraInicio=23:59
HoraFin=06:00
Activo=True
IniciarConWindows=True

### ⚠️ Nota Importante sobre Proyectos Experimentales y Bucles de Apagado

Al tratarse de una solución experimental que interactúa de manera forzada e instantánea con los comandos del sistema operativo (`shutdown.exe`), se debe tener especial precaución al configurar los rangos:

*   **Comportamiento en Rango Activo:** Si se activa la restricción y el reloj actual del equipo coincide con el horario prohibido, el programa ejecutará el apagado en un ciclo continuo cada vez que inicie Windows.
*   **Margen de Maniobra:** Debido a que el sistema se cierra de inmediato en un bucle, **no habrá tiempo suficiente dentro de la interfaz gráfica para desactivar la aplicación o realizar cambios de manera convencional**.
*   **Soluciones en caso de bloqueo:** Si quedas atrapado en el rango restrictivo por error, tienes dos opciones:
    1.  **Esperar a que expire el tiempo:** Dejar el equipo apagado hasta que el reloj real del sistema supere la `HoraFin` configurada. Al encenderlo fuera del horario prohibido, podrás abrir la interfaz normalmente mediante el icono del reloj y corregir los valores.
    2.  **Modificación Externa de Emergencia:** Iniciar el equipo en **Modo Seguro con Símbolo del Sistema** (o conectar el disco a otro equipo) para acceder al directorio del programa y editar o eliminar manualmente el archivo `config.ini` o el ejecutable antes de que Windows inicie la carga normal de procesos.
