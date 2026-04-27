# Booking Prototipo

## 1. Descripción

Booking Prototipo es una plataforma académica tipo marketplace orientada a la integración de múltiples sistemas de información, basada en un enfoque API-first y con evolución hacia una arquitectura de microservicios.

El sistema se inspira en plataformas como Booking, pero adaptado al contexto de la asignatura de Integración de Sistemas, permitiendo la interoperabilidad entre distintos sistemas desarrollados de forma independiente.

---

## 2. Objetivo

El objetivo del proyecto es diseñar e implementar un ecosistema digital que permita:

- Integración de múltiples sistemas
- Consumo e interoperabilidad mediante APIs
- Escalabilidad mediante microservicios
- Evolución arquitectónica progresiva
- Despliegue en entornos web y móviles

---

## 3. Arquitectura del sistema

El proyecto se compone de dos niveles principales:

### 3.1 Sistemas individuales

Cada sistema desarrollado incluye:

- Backend con APIs expuestas
- Módulo de administración
- Interfaz tipo marketplace
- Base de datos independiente
- Despliegue público en internet

### 3.2 Sistema central (Booking Prototipo)

- Plataforma integradora
- Consumo de APIs externas
- Agregación de información
- Marketplace centralizado

---

## 4. Tecnologías utilizadas

### Backend
- C# / .NET
- Arquitectura en capas
- APIs REST
- OpenAPI (Swagger)
- CORS

### Frontend
- Angular

### Enfoque arquitectónico
- API-first
- Microservicios (en evolución)
- REST

Preparado para integración futura con:
- gRPC
- GraphQL
- Arquitectura orientada a eventos (EDA)

---

## 5. Estructura del proyecto


Microservicio.Coche/
├── backend/
│ ├── Microservicio.Coche.API/
│ └── (otros microservicios)
│
├── frontend/
│ └── booking-vehiculos/
│
├── .gitignore
└── README.md



---

## 6. Estado del proyecto

### Reto 1: API-first sin integración

El sistema actualmente incluye:

- Backend funcional con APIs documentadas
- Sistema de administración operativo
- Marketplace web funcional
- Base de datos configurada
- Despliegue en entorno accesible
- Preparación para integración futura

---

## 7. Diseño de APIs

Las APIs siguen buenas prácticas REST:

- Uso adecuado de verbos HTTP
- Definición de recursos
- Versionamiento
- Manejo de códigos de estado
- Documentación mediante Swagger/OpenAPI

---

## 8. Evolución arquitectónica

El proyecto está diseñado para evolucionar hacia:

- Arquitectura de microservicios
- Integración síncrona y asíncrona
- Implementación de eventos (EDA)
- Uso de gRPC para comunicación eficiente
- GraphQL para consultas agregadas
- API Gateway
- Seguridad en integraciones
- Observabilidad y resiliencia
- Aplicación móvil (Flutter o React Native)

---

## 9. Contenidos aplicados

Este proyecto integra los siguientes conceptos:

- Integración de sistemas
- Enfoque API-first
- REST avanzado
- SOA y microservicios
- Arquitectura orientada a eventos
- Contratos de integración
- Interoperabilidad entre sistemas

---

## 10. Documentación técnica

El proyecto incluye:

- Definición de arquitectura inicial
- Modelo de datos
- Contratos de APIs
- Identificación de puntos de integración
- Propuesta de evolución a microservicios

---

## 11. Consideraciones

Se excluyen del repositorio archivos generados automáticamente:

- node_modules/
- bin/
- obj/
- .vs/
