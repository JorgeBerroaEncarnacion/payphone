
# 📘 Documentación de la API – Sistema de Pedidos

## Introducción

Este sistema está dividido en tres módulos principales expuestos mediante controladores:

- `CustomerController` → Maneja la creación y consulta de clientes.
- `PedidosController` → Maneja las operaciones CRUD sobre pedidos.
- `HistorialController` → Registra y obtiene el historial de estado de un pedido.

Además, se **creó la clase `Customer`** porque para poder crear un pedido (`Pedido`), es necesario que este esté asociado a un cliente. El modelo de negocio exige que cada pedido pertenezca a un cliente registrado, garantizando así la trazabilidad y la asociación entre pedidos e identidad del comprador.

---

## 📍 CustomerController

**Base URL:** `/api/customer`

### ➕ POST `/api/customer`

**Descripción:** Crea un nuevo cliente.

**Body (JSON):**
```json
{
  "Name": "Juan Pérez"
 
}
```

**Respuesta (200 OK):**
```json
{
  "Id": 1,
  "Name": "Juan Pérez"
}
```

---

### 🔍 GET `/api/customer/{id}`

**Descripción:** Obtiene un cliente por su ID.

**Respuesta (200 OK):**
```json
{
  "Id": 1,
  "Name": "Juan Pérez"
}
```

---

## 📦 PedidosController

**Base URL:** `/api/pedidos`

> **Nota:** Antes de crear un pedido, el cliente debe existir (creado mediante `CustomerController`).

### ➕ POST `/api/pedidos`

**Descripción:** Crea un nuevo pedido.

**Body (JSON):**
```json
{
  "CustomerId": 1,
  "TotalAmount": 2550
}
```

**Respuesta (200 OK):**
```json
{
  "Id": 10,
  "CustomerId": 1,
  "TotalAmount": 2550,
  "Status": 0,
"createdAt": "2025-03-21T17:53:04.559602",
  "updatedAt": "2025-03-21T22:11:25.41025"

}
```

---

### 🔍 GET `/api/pedidos/{id}`

**Descripción:** Obtiene un pedido por ID.
```json
{
  "Id": 10,
  "CustomerId": 1,
  "TotalAmount": 2550,
  "Status": 0,
"createdAt": "2025-03-21T17:53:04.559602",
  "updatedAt": "2025-03-21T22:11:25.41025"

}
```
---

### 🔄 PUT `/api/pedidos`

**Descripción:** Actualiza un pedido existente.

**Body (JSON):**
```json
{
  "id": 10,
  "totalAmount": 200
}
```
**Respuesta (200 OK):**
```json
{
  "Id": 10,
  "CustomerId": 1,
  "TotalAmount": 2550,
  "Status": 0,
"createdAt": "2025-03-21T17:53:04.559602",
  "updatedAt": "2025-03-21T22:11:25.41025"

}
```
---

### ❌ DELETE `/api/pedidos/{id}`

**Descripción:** Elimina un pedido por ID.

**Respuesta (200 OK):**
---

## 📜 HistorialController

**Base URL:** `/api/historial`

### ➕ POST `/api/historial`

**Descripción:** Crea un nuevo estado en el historial para un pedido.

**Body (JSON):**
```json
{
  "orderId": 10,
  "newStatus": 1
}
```
**Respuesta (200 OK):**

```json
{
  "id": 3,
  "orderId": 10,
  "previousStatus": 0,
  "newStatus": 1,
  "changeAt": "2025-03-22T12:55:02.4057955-04:00"
}
```

---

### 🔍 GET `/api/historial/{pedidoId}`

**Descripción:** Obtiene el historial de estados de un pedido.

**Respuesta:**
```json
[
  {
    "id": 13,
    "orderId": 3,
    "previousStatus": 1,
    "newStatus": 2,
    "changeAt": "2025-03-22T12:55:02.256133"
  },
  {
    "id": 14,
    "orderId": 3,
    "previousStatus": 2,
    "newStatus": 3,
    "changeAt": "2025-03-22T12:56:19.135366"
  }
]
```

---

## ✅ Relación entre `Customer` y `Pedido`

Un **pedido no puede existir sin un cliente**. Es por eso que primero se debe crear un `Customer` y luego, usando su `id`, se puede generar un `Pedido`. Esto asegura una correcta asociación de pedidos con usuarios y permite gestionar correctamente tanto el historial como la trazabilidad de cada transacción.
