# Prototipo - Interfaces Inteligentes

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
8. [Sensores y Interfaces Multimodales](#sensores-y-interfaces-multimodales)  
9. [Checklist de Recomendaciones de Diseño VR](#checklist-de-recomendaciones-de-diseño-vr)  
10. [Aspectos Destacables y Hitos de Programación](#aspectos-destacables-y-hitos-de-programación)  

---

## Descripción General
*"El Último Brindis en la Suite 602"* es un prototipo de juego de misterio en VR desarrollado en **Unity 6**, diseñado para sumergir al jugador en una experiencia de investigación de crimen tipo **Cine Noir**.  
El jugador debe investigar la muerte de **Julian Vane**, un marchante de arte y blanqueador de capitales, interactuar con objetos y NPCs, descubrir pistas digitales y físicas, y finalmente identificar al culpable antes de que el sistema de seguridad se reinicie.  

El proyecto combina narrativa inmersiva, interacción física con pistas en VR y lógica de interrogatorio basada en un **sistema de pistas dinámico**.

---

## Objetivo del Juego
- **Recuperar el Libro de Cuentas** digital que está protegido por biometría.  
- **Acusar correctamente al culpable** antes de que las puertas de seguridad se desbloqueen.  
- **Explorar y analizar pistas** distribuidas en la oficina de Julian.  
- **Interrogar a los NPCs**, aumentando o disminuyendo su nivel de sospecha dependiendo de las pistas que se les presenten.  

---

## Escenario y Narrativa
- Ubicación: Oficina privada de Julian Vane (Suite 602).  
- Escena del crimen: Cuerpo de Julian sobre su escritorio, copa de coñac volcada y quemadura en la muñeca izquierda.  
- Dinámica principal: Los jugadores deben combinar pistas físicas (objetos VR) y digitales (grabaciones de voz, inhibidores de señal, notas) para resolver el caso.  
- Atmósfera: Cine Noir con iluminación URP en tiempo real, sombras suaves y contrastes dramáticos.

---

## Pistas y NPCs

### Pistas Clave
| ID | Nombre | Descripción | NPC Relacionado |
|----|--------|-------------|----------------|
| CLUE_01_GEMELO | Gemelo Desparejado | Gemelo de oro bajo la alfombra | Marcus |
| CLUE_02_COPA | Copa con Lápiz Labial | Restos carmín de lápiz labial | Elena |
| CLUE_03_INHIBIDOR | Inhibidor de Señal | Dispositivo electrónico escondido | Leo |
| CLUE_04_NOTA | Nota de Chantaje | Papel arrugado con amenaza | Todos |
| CLUE_05_GRABADOR | Grabador de Voz VR | Reproduce últimos 10s de audio con voz femenina | Elena |

### NPCs
1. **Marcus “La Roca”**  
   - Rol: Guardaespaldas personal  
   - Personalidad: Estoico, nervioso, evita contacto visual con cadáver  
   - Oculta: Gemelo perdido  

2. **Elena Vance** *(culpable)*  
   - Rol: Socia y ex-esposa  
   - Personalidad: Fría, calculadora, elegante  
   - Oculta: Uso del taser, dispositivo biométrico, voz grabada  

3. **Leo “Bits”**  
   - Rol: Experto en seguridad/hacker  
   - Personalidad: Hiperactivo, paranoico  
   - Oculta: Instalación del inhibidor por orden de Elena  

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
- GameDataManager: Script central que:
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

## Aspectos Destacables y Hitos de Programación

- Integración de un JSON centralizado que controla todo el flujo de pistas y NPCs.
- Uso de XR Interaction Toolkit + Sentis para interacción física e interrogatorio dinámico.
- Implementación de un sistema de sospecha de NPCs, que cambia diálogo y comportamientos según acciones del jugador.
- Creación de UI en VR para diagnosticar progreso de pistas sin romper inmersión.
- Diseño modular de datos (GameStateRoot) que facilita escalabilidad y futuras expansiones del juego.

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
| Feedback auditivo espacial    | ✅ Se contempla |
| Reconocimiento de voz         | ✅ Se contempla |
| Seguimiento corporal completo | ❌ No aplica   |
