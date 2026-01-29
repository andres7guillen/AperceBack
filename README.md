# AperceBack
No usé PUT /tasks/{id}/status porque exponer el estado directamente convierte una regla de negocio en un dato modificable. En cambio, usé endpoints por intención (start, complete) que representan acciones del dominio. Esto hace que la API sea más explícita, evita transiciones inválidas y mantiene el control en el aggregate
