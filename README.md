# SUPCOUNT

## Schéma de base de données : Identity (Authentification)

![alt text](images/DiagramAuth.png)

Ce modèle gère l’authentification et l’autorisation des utilisateurs via des tables liées aux rôles, revendications, connexions externes et tokens. Il assure une sécurité avancée et une gestion flexible des utilisateurs.

## 🔒 **1. `User` (utilisateur)**

Contient les informations sur les utilisateurs de l’application.

### Principaux attributs :

- `Id`: identifiant unique de l’utilisateur.
- `UserName`, `NormalizedUserName`: nom d’utilisateur.
- `Email`, `NormalizedEmail`, `EmailConfirmed`: email de l'utilisateur et confirmation.
- `PasswordHash`: mot de passe hashé.
- `SecurityStamp`, `ConcurrencyStamp`: utilisés pour la sécurité et la gestion de concurrence.
- `PhoneNumber`, `PhoneNumberConfirmed`: numéro de téléphone.
- `TwoFactorEnabled`: double authentification.
- `LockoutEnabled`, `LockoutEnd`: gestion du verrouillage de compte.
- `AccessFailedCount`: nombre d’échecs de connexion.
- `FullName`: nom complet de l’utilisateur.

---

## 🔑 **2. `UserLogins` (connexions externes)**

Permet de se connecter via des fournisseurs externes (Google, Facebook...).

### Attributs :

- `UserId`: FK vers `User`.
- `ProviderKey`, `LoginProvider`, `ProviderDisplayName`: infos du fournisseur externe.

---

## 🔐 **3. `UserTokens`**

Stocke des tokens d’authentification pour les utilisateurs.

### Attributs :

- `UserId`: FK vers `User`.
- `LoginProvider`, `Name`, `Value`: détails du token.

---

## 👤 **4. `UserClaims`**

Décrit des revendications (claims) spécifiques à l'utilisateur, utilisées pour l'autorisation.

### Attributs :

- `Id`: identifiant de la revendication.
- `ClaimType`, `ClaimValue`: type et valeur.
- `UserId`: FK vers `User`.

---

## 🧑‍🤝‍🧑 **5. `Roles`**

Définit les rôles (Admin, Utilisateur, etc.) dans le système.

### Attributs :

- `Id`: identifiant du rôle.
- `Name`, `NormalizedName`: nom du rôle.
- `ConcurrencyStamp`: pour la gestion de la concurrence.

---

## 🔄 **6. `UserRoles`**

Table de jointure entre `User` et `Roles` → Un utilisateur peut avoir plusieurs rôles, et un rôle peut appartenir à plusieurs utilisateurs.

### Attributs :

- `UserId`: FK vers `User`.
- `RoleId`: FK vers `Roles`.

---

## 📜 **7. `RoleClaims`**

Décrit les revendications associées à un rôle.

### Attributs :

- `Id`: identifiant.
- `ClaimType`, `ClaimValue`: type et valeur de la revendication.
- `RoleId`: FK vers `Roles`.

---

## 🔗 Relations :

- **User** :
  - 1 utilisateur peut avoir plusieurs `UserClaims`, `UserLogins`, `UserTokens`, `UserRoles`.
- **Roles** :
  - 1 rôle peut être attribué à plusieurs utilisateurs (`UserRoles`) et avoir plusieurs `RoleClaims`.

---

## ✅ Utilité globale :

Ce schéma est **standard dans les applications ASP.NET Core utilisant Identity**. Il permet :

- Une gestion complète des utilisateurs et de la sécurité.
- La prise en charge de l’authentification externe.
- L’autorisation via rôles et revendications personnalisées.
- Une sécurité robuste avec le verrouillage de compte et l’authentification à deux facteurs.


## Diagramme UML : Diagramme de classes 

![alt text](images/DiagramUML.jpg)

## 👥 **`8. Group` (groupe)** 

Représente un groupe d’utilisateurs .

### Attributs :

- `Id`: identifiant du groupe.
- `Name`: nom du groupe.
- `Description`: description facultative du groupe.

## 🔁 **`9. UserGroup`**
Table de jointure entre User et Group avec rôle dans le groupe.

### Attributs :
- `UserId`: FK vers User.
- `GroupId`: FK vers Group.
- `Role`: rôle du membre (ex: admin, membre).
- `CreatedAt`: date d'entrée dans le groupe.

## 💸 **`10. Expense` Depense **
Une dépense effectuée dans un groupe, par un utilisateur.

### Attributs :

- `Id`: identifiant.
- `Title`: nom ou objet de la dépense.
- `Amount`: montant total.
- `Date`: date de la dépense.
- `PayerId`: FK vers l’utilisateur ayant payé.
- `GroupId`: FK vers le groupe concerné.
- `CategoryId`: FK vers une catégorie (facultatif).

## 🧾 **`11. Receipt` **
Justificatif associé à une dépense.

### Attributs :

- `Id`: identifiant.
- `ExpenseId`: FK vers Expense.
- `FilePath`: chemin du fichier (URL, local...).
- `Type`: format ou nature du justificatif (ex: image, PDF…).

## 🧮 **`12. Participation`**
Représente la part d’un utilisateur dans une dépense.

### Attributs :

- `UserId`: FK vers User.
- `ExpenseId`: FK vers Expense.
- `Weight`: pondération (ex: 1 = part égale, 0.5 = moitié…).

## 🗂️ **`13. Category`**
Catégorisation facultative des dépenses (ex : Transport, Courses…).

### Attributs :

- `Id`: identifiant.
- `Name`: nom de la catégorie.

## 💰 **`14. Reimbursement` (Remboursement)**
Remboursement d’un utilisateur vers un autre dans un groupe.

### Attributs :

- `Id`: identifiant.
- `SenderId`: utilisateur qui rembourse.
- `BeneficiaryId`: utilisateur remboursé.
- `Amount`: montant.
- `GroupId`: FK vers le groupe.

## 💳 **`15. Transaction`**
Représente une opération réelle liée à un remboursement.

### Attributs :

- `Id`: identifiant.
- `ReimbursementId`: FK vers Reimbursement.
- `PaymentMethod`: mode de paiement utilisé.
- `Amount`: montant versé.
- `Date`: date de la transaction.

## 💬 **`16. Message`**
Messagerie interne (privée ou de groupe).

### Attributs :

- `Id`: identifiant.
- `Content`: contenu du message.
- `SentAt`: date d’envoi.
- `GroupId`: FK vers un groupe (si message de groupe).
- `SenderId`: FK vers l’expéditeur (User).
- `RecipientId`: FK vers un utilisateur (si message privé).

## 🔗 Relations principales :
- Un groupe peut contenir plusieurs utilisateurs (UserGroup), dépenses, messages et remboursements.

- Une dépense est liée à un payeur, un groupe, des participants (Participation), un justificatif, et une catégorie.

- Un remboursement peut générer plusieurs transactions.

- Un utilisateur peut envoyer/recevoir des messages, participer à des dépenses, appartenir à des groupes et effectuer des remboursements.
