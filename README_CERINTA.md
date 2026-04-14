# Cerință automatizare "The Farmer Was Replaced"

Vreau un script complet de automatizare pentru jocul "The Farmer Was Replaced" care:

- Rulează în buclă infinită și citește dinamic dimensiunea fermei cu `get_world_size()`
- Respectă layout-ul pe coloane:
  - coloane 1–2: Carrot
  - coloane 3–4: Tree
  - coloane 5–6: Sunflower
  - coloane 7–8: Grass
  - colț dreapta-sus 2x2: Pumpkin (override)
- Face mapare dinamică celulă → cultură țintă, adaptabilă la extinderi
- Aplică `till()` doar o singură dată pe celulele care necesită arat
- După inițializare, rulează ciclul normal fără `till()` repetat: `harvest()` → `plant()`
- Udă condiționat:
  - Tree: sub 50%
  - Pumpkin sub 60%
- Deblochează automat unlock-uri utile în ordine de prioritate (ex: Speed, Expand, MegaFarm)
- Rulează repetitiv pașii:
  1) verificare unlock-uri
  2) parcurgere fermă
  3) întreținere pe celulă
  4) reluare buclă
- La creșterea hărții, initializează doar celulele noi (inclusiv `till()` inițial)
- Folosește traseu serpentine/zig-zag pentru eficiență
- Include protecții simple de stabilitate (verificări stare, retry scurt, evitare blocaj)
