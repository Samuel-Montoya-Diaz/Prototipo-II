# Prototipo - Interfaces Inteligentes 2025/2026

## Autores
- Carolina Acosta Acosta
- Samuel Frías Hernández
- Guillermo Silva González
- Samuel Montoya Díaz

---

# README - Proyecto VR: *El Misterio: "El Último Brindis en la Suite 602"*

## Índice
1. [Descripción General](#descripción-general)  
2. [Objetivo del Juego](#objetivo-del-juego)  
3. [Escenario y Narrativa](#escenario-y-narrativa)  
4. [Pistas y NPCs](#pistas-y-npcs)  
5. [Arquitectura de Datos](#arquitectura-de-datos)  
6. [Implementación Técnica en Unity 6](#implementación-técnica-en-unity-6)  
7. [Integración de Interacciones VR](#integración-de-interacciones-vr)  
8. [Checklist de Recomendaciones de Diseño VR](#checklist-de-diseño-vr)  
9. [Aspectos Destacables y Hitos de Programación](#aspectos-destacables-y-hitos-de-programación)
10. [Acuerdos del Grupo](#acuerdos-del-grupo)  

---

## Descripción General
*"El Último Brindis en la Suite 602"* es un prototipo de juego de misterio en VR desarrollado en **Unity 6**, diseñado para sumergir al jugador en una experiencia de investigación de crimen tipo **Cine Noir**.  
El jugador debe investigar la muerte de **Julian Vane**, un traficante de arte y blanqueador de capitales, interactuar con objetos y NPCs, descubrir pistas digitales y físicas, y finalmente identificar al culpable antes de que el sistema de seguridad se reinicie.  

El proyecto combina narrativa inmersiva, interacción física con pistas en VR y lógica de interrogatorio basada en un **sistema de pistas dinámico**.

---

## Objetivo del Juego
- **Recuperar el Libro de Cuentas** digital que está protegido por biometría.  
- **Acusar correctamente al culpable** antes de que las puertas de seguridad se desbloqueen.  
- **Explorar y analizar pistas** distribuidas en el piso de Julian.  
- **Interrogar a los NPCs**, aumentando o disminuyendo su nivel de sospecha dependiendo de las pistas que se les presenten.  

---

## Escenario y Narrativa
- Ubicación: Piso de Julian Vane (Suite 602).  
- Escena del crimen: Cuerpo de Julian en el suelo, dos copas de vino en una bandeja, y quemadura en la muñeca izquierda.  
- Dinámica principal: Los jugadores deben combinar pistas físicas (objetos VR) y digitales (grabaciones de voz, inhibidores de señal, notas) para resolver el caso.  
- Atmósfera: Cine Noir con iluminación URP en tiempo real, sombras suaves y contrastes dramáticos.

### Ilustraciones

A continuación, una muestra de las pistas según cómo era la historia original, y a cómo se podría escalar a partir del prototipo.

![Grabador](Ilustraciones/grabador.png)
![Nota](Ilustraciones/nota.png)
![Inhibidor](Ilustraciones/inhibidor.png)
![Copa](Ilustraciones/copa.png)
![Gemelo](Ilustraciones/gemelo.png)
---

## Pistas y NPCs

### Pistas Clave
| ID | Nombre | Descripción | NPC Relacionado |
|----|--------|-------------|----------------|
| CLUE_01_GEMELO | Gemelo de Camiseta Desparejado | Gemelo de oro bajo la alfombra | Marcus |
| CLUE_02_COPA | Copa con Lápiz Labial | Restos carmín de lápiz labial | Elena |
| CLUE_03_INHIBIDOR | Inhibidor de Señal | Dispositivo electrónico escondido | Leo |
| CLUE_04_NOTA | Nota de Chantaje | Papel arrugado con amenaza | Todos |
| CLUE_05_GRABADOR | Grabador de Voz VR | Reproduce últimos 10s de audio con voz femenina | Elena |

### NPCs
1. **Marcus “La Roca”**  
   - Rol: Guardaespaldas personal  
   - Personalidad: Estoico, nervioso, evita contacto visual con cadáver  
   - Oculta: Gemelo de camiseta perdido  

2. **Elena Vance** *(culpable)*  
   - Rol: Socia y ex-esposa  
   - Personalidad: Fría, calculadora, elegante  
   - Oculta: Uso del taser, dispositivo biométrico, voz grabada  

3. **Leo “Bits”**  
   - Rol: Experto en seguridad/hacker  
   - Personalidad: Hiperactivo, paranoico  
   - Oculta: Instalación del inhibidor, por orden de Elena  

---

## Arquitectura de Datos
Se utiliza un **JSON “Single Source of Truth”** que representa el estado completo del juego, incluyendo:  
- Metadata del caso  
- Estado global de pistas  
- Estado de NPCs y su nivel de sospecha  
- Hitos narrativos  
- Lógica de victoria

### Ejemplo de Estructura
```json
{
  "case_metadata": { ... },
  "global_logic": { ... },
  "clues_state": [ ... ],
  "npc_states": { ... },
  "narrative_milestones": { ... }
}
```
---

## Implementación Técnica en Unity 6

- XR Interaction Toolkit 3.0: Manejo de interacciones VR como agarrar y acercar objetos.
- URP (Universal Render Pipeline): Iluminación en tiempo real con estilo Cine Noir.
- Sentis: Para interacción avanzada con NPCs, mostrando reacciones a pistas acercadas físicamente.
- Clases Serializables en C#: Para mapear el JSON y manejar estados de juego y NPCs.
- GameDataManager: Script central que
  - Carga y guarda JSON
  - Actualiza pistas
  - Actualiza hitos narrativos
  - Gestiona lógica de sospecha y victoria

--- 

## Integración de Interacciones VR

- Objetos Interactivos: XR Grab Interactable para pistas.
- Interrogatorio: Acercar pistas a NPCs modifica suspicion_level.
- Eventos: On Select Entered dispara MarkClueAsFound(clueId) en GameDataManager.
- UI de Diagnóstico: Panel VR flotante que muestra progreso (clues_found_count).

---

## Checklist de Diseño VR

| Funcionalidad / Sensor        | Estado           |
|-------------------------------|----------------|
| Head Tracking                 | ✅ Se contempla |
| Feedback visual               | ✅ Se contempla |
| Control de movimiento         | ✅ Se contempla |
| Velocidad constante           | ✅ Se contempla |
| Brillo / Iluminación          | ✅ Se contempla |
| Interacción con objetos       | ✅ Se contempla |
| Feedback auditivo espacial    | ❌ no se contempla |
| Reconocimiento de voz         | ✅ Se contempla |
| Seguimiento corporal completo | ❌ No aplica   |

---

## Aspectos Destacables y Hitos de Programación

- Integración de un JSON centralizado que controla todo el flujo de pistas y NPCs.
- Uso de XR Interaction Toolkit + Sentis para interacción física e interrogatorio dinámico.
- Implementación de un sistema de sospecha de NPCs, que cambia diálogo y comportamientos según acciones del jugador.
- Creación de UI en VR para diagnosticar progreso de pistas sin romper inmersión.
- Diseño modular de datos (GameStateRoot) que facilita escalabilidad y futuras expansiones del juego.

---

## Acuerdos del Grupo

Se ha realizado la siguiente división de tareas, para poder organizar el trabajo mejor, pero realmente todos han acabado participando en cada parte de una forma u otra:

### Persona 1 – Unity / VR / Escena
**Responsabilidades principales:**
- Crear y mantener el proyecto en Unity con integración VR (Meta XR SDK).
- Montar y configurar la escena principal, incluyendo iluminación y posición del jugador.
- Colocar los NPCs interactivos en la escena y configurar sus prefabs.
- Implementar la lógica básica de interacción con NPCs y objetos.
- Desarrollar UI básica para diálogos y feedback visual de pistas.
- Gestionar el ClueManager para actualizar estados de pistas.

**Tareas clave:**
- Crear proyecto Unity + Meta XR SDK.  
- Importar paquetes de assets y montar la escena.  
- Configurar player spawn e iluminación básica.  
- Colocar 3 NPCs (prefabs simples).  
- Script NPCInteraction: detectar proximidad y seleccionar NPC activo.  
- Implementar UI básica de diálogo y feedback visual de pistas.

---

### Persona 2 – Backend / IA
**Responsabilidades principales:**
- Desarrollar el backend que gestiona la lógica de diálogo y transcripción de voz.  
- Integrar Whisper y el LLM para permitir diálogos por voz con NPCs.  
- Gestionar endpoints y prompts dinámicos, así como condiciones de revelado de pistas.  
- Optimizar latencia y coherencia de respuestas del LLM.  

**Tareas clave:**
- Configurar backend mínimo (Express / FastAPI).  
- Crear endpoint `/transcribe` para Whisper y `/dialogue` para diálogos.  
- Implementar prompt base común a todos los NPCs.  
- Entregar respuestas estructuradas con información sobre diálogos y pistas reveladas.  
- Manejo de estados del misterio y control de incoherencias del LLM.

---

### Persona 3 – Narrativa / Diseño del Misterio
**Responsabilidades principales:**
- Definir y estructurar la narrativa del misterio.  
- Crear las pistas clave y asignarlas a los NPCs.  
- Redactar fichas de NPC detallando personalidad, conocimientos y secretos.  
- Ajustar diálogos, frases evasivas y finales de la historia para la demo.  

**Tareas clave:**
- Definir el misterio en un párrafo claro y comprensible.  
- Crear 3–4 pistas claras y asignarlas a los NPCs correspondientes.  
- Redactar fichas de NPC (personalidad, qué sabe, qué oculta).  
- Mantener los estados globales del caso en JSON para integración con Unity y backend.  
- Redactar el guión corto de demo para la presentación.

---

### Persona 4 – Integración / QA / Demo
**Responsabilidades principales:**
- Asegurar que todos los sistemas funcionen correctamente en conjunto (Unity + Backend + Narrativa).  
- Realizar pruebas en hardware VR (Quest) para detectar errores y riesgos técnicos.  
- Proponer soluciones o fallbacks cuando algo falle (por ejemplo, texto si la voz no funciona).  
- Preparar la demo y validar la experiencia final.  

**Tareas clave:**
- Probar escena en Quest y validar interacciones básicas.  
- Probar backend de manera manual y forzar casos límite del LLM.  
- Ajustar prompts con Persona 2 según resultados de QA.  
- Verificar coherencia narrativa y estabilidad del prototipo.  
- Preparar ensayo de demo de 3–5 minutos y explicación de automatización y escalabilidad.
