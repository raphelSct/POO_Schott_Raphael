# Utilisation de l'IA – TP1 Introduction C#

- **Étudiant :** Raphaël Schott
- **Projet :** `Schott_Raphael_Tp1`
- **Outil :** Claude (modèle Claude Opus 5.5), utilisé via Claude Code dans l'application de bureau Claude
- **Date :** 9 novembre 2026

L'assistant avait accès au dossier du projet : il pouvait lire mes fichiers, les modifier, compiler et lancer le programme. Je travaillais en parallèle dans VS Code sur les mêmes fichiers.

## 1. Comment j'ai utilisé l'IA

J'ai utilisé l'IA de trois façons :

- **Quand je ne savais pas faire ou que j'étais bloqué** : je lui posais une question sur une notion ou une erreur, et elle m'expliquait le problème, parfois avec un court exemple de code que j'intégrais ensuite.
- **Pour générer du code que j'aurais pu écrire mais qui me demandait du temps** : initialisation du projet, fonctions d'affichage, partie 8, liste d'armes de l'armurerie, modificateurs de dégâts.
- **Pour relire et corriger** : après certaines parties, puis sur tout le projet avant le bonus, je lui ai demandé de vérifier le respect du sujet, des conventions de nommage et la propreté du code, et d'appliquer les corrections nécessaires.

## 2. Détail par partie du sujet

| Partie | Écrit par moi | Apport de l'IA |
|---|---|---|
| 0. Projet | — | Création de la solution, du projet console `Schott_Raphael_Tp1`, des dossiers et des fichiers de classes vides |
| 1. Player | `FirstName`, `LastName`, `Alias`, constructeur, `FormatName`, `Name`, `ToString` | Explication de mon blocage sur la méthode `static` (pas de `this`) ; oubli du `ToLower` signalé ; code de `Equals` (le mien était faux) et de `GetHashCode`, donnés avec l'explication du contrat entre les deux ; `FirstName` et `LastName` passés de champs à propriétés privées lors de la relecture |
| 2. SpaceInvaders | Classe, liste de joueurs, constructeur, `Main`, boucle d'affichage, suppression de `Program.cs` | Contenu de `Init()` (3 joueurs) ; version `foreach` de ma boucle `for` |
| 3. Spaceship | Propriétés et constructeur | Corrections lors de la relecture (voir partie 4) |
| 4. Weapon / EWeaponType | Classe, enum, constructeur, dégâts moyens | Corrections lors de la relecture (voir partie 4) |
| 5. Armory | Classe, `Init()`, `AddWeapon`, `RemoveWeapon` | Liste jamais instanciée corrigée, `ViewArmory()`, exposition de la liste en `IReadOnlyList` |
| 6. Spaceship (armes) | Liste privée exposée en lecture seule (d'après l'explication de l'IA), `AddWeapon`, `RemoveWeapon`, `ClearWeapons` | `ViewWeapons()`, `ViewShip()`, `Weapon.ToString()`, exception à la 4e arme ; ligne de calcul de `AverageDamages` ; rédaction de la réponse à la question 6.6 |
| 7. Player (vaisseau) | Propriété `BattleShip` et vaisseau par défaut | Passage de `BattleShip` en propriété publique |
| 8. SpaceInvaders | — | Partie écrite par l'IA |
| 9. Weapon (tir) | `ReloadTime`, `TimeBeforeReload`, `DecrementReload`, `IsReady`, première version de `Shoot()` (à partir de la formule et du squelette expliqués par l'IA) | Modificateurs selon le type d'arme, rechargement doublé des armes explosives, 6 armes du tableau dans l'armurerie ; `IsReady` et `TimeBeforeReload` passés en propriétés |
| 10. Interfaces | Implémentation des trois interfaces | Explication de la visibilité imposée par une interface ; correction des namespaces |
| 11.a Bonus | `ArmoryException` et intégration dans le projet | Exemple de classe d'exception, comparaison des façons de donner l'armurerie au vaisseau, recommandation du membre `static`, code de `Contains` et de la vérification dans `AddWeapon` |

La partie 2.7 (point d'arrêt et pas-à-pas dans le débogueur) se fait à la main dans le débogueur : l'IA n'y a pas participé.

## 3. Notions expliquées par l'IA

- **Autocomplétion absente dans VS Code** : aucun import ne manquait, car `ImplicitUsings` importe déjà `System`. L'IA a lu les journaux de l'extension C# : le dossier était ouvert en mode restreint, et il fallait lui faire confiance pour que l'analyseur démarre.
- **Méthode `static`** : elle n'a pas de `this`. La méthode doit recevoir le nom en paramètre et renvoyer le nom formaté.
- **`Equals` et `GetHashCode`** : deux objets égaux doivent avoir le même hash, donc le hash se calcule sur les mêmes champs que la comparaison (ici le pseudo).
- **Liste en lecture seule** : une `List` privée modifiée par les méthodes de la classe, exposée à l'extérieur par une propriété `IReadOnlyList`.
- **Auto-propriété ou propriété calculée** : `{ get; }` crée une valeur stockée, remplie seulement dans le constructeur, alors que `=>` recalcule la valeur à chaque lecture. C'est la cause de plusieurs bugs listés plus bas.
- **Tirage aléatoire entre deux bornes** : `min + Random.Shared.NextDouble() * (max - min)`.
- **Visibilité et interfaces** : une interface impose un `get` public, mais le `set` peut rester privé.
- **Exceptions personnalisées** : héritage d'`Exception`, transmission du message avec `base(...)`, ordre des blocs `catch`.
- **Question 6.6** : `List.Remove` compare avec `Equals`, donc par référence tant que `Weapon` ne le redéfinit pas.

## 4. Erreurs trouvées grâce à la relecture

Plusieurs de ces bugs compilaient sans erreur mais donnaient de mauvais résultats à l'exécution.

- `Equals` comparait le pseudo (une `string`) à un objet `Player` : le résultat était toujours `false`.
- Listes de joueurs et de l'armurerie jamais instanciées, ce qui provoquait une `NullReferenceException`.
- Dégâts minimum et maximum inversés à la création des armes.
- Paramètre du constructeur nommé comme le champ (`ReloadTime = ReloadTime;`) : le temps de rechargement restait à 0.
- `AverageDamages`, `AverageDamage` et `IsDestroyed` déclarées en `{ get; }` sans calcul : elles valaient toujours 0 ou `false`.
- `CurrentShield` et `CurrentStructure` ne pouvaient plus changer après le constructeur, alors que le sujet prévoit des dégâts et des réparations.
- Nommage : fautes de frappe (`MaxDamge`, `AverageDamge`, `ReloadTIme`, `maxStrucutre`), méthodes `isDestroyed()` et `isReady()` à transformer en propriétés, champs privés écrits en PascalCase, fichier `Iplayer.cs` mal nommé.
- Mise en forme irrégulière (accolades, espaces, lignes vides, `this.` inutiles), harmonisée lors de la relecture finale.

## 5. Erreurs de l'IA et contenu à signaler

- **Namespaces** : à l'initialisation, l'IA a créé des namespaces préfixés par le nom du projet (`Schott_Raphael_Tp1.Models`, …) au lieu de `Models`, `Models.SpaceShips` et `SpaceInvadersArmory` demandés par le sujet. Pour compiler avec les interfaces fournies, des `using` avaient alors été ajoutés dans ces interfaces. L'erreur a été corrigée lors de la relecture finale : les `using` ajoutés dans les interfaces ont été retirés ou remplacés par leur équivalent sans préfixe.
- **Textes rédigés à partir de l'IA** : la réponse à la question 6.6 (commentaire au-dessus de `RemoveWeapon` dans `Spaceship.cs`) a été rédigée par l'IA. Les arguments du commentaire qui justifie le choix du membre `static` (bonus) reprennent ceux qu'elle m'a proposés.

## 6. Vérification du code

- Après chaque modification faite par l'IA : compilation (`dotnet build`) et exécution du jeu (`dotnet run`).
- L'IA a écrit de petits programmes de test, à part et hors du dépôt, pour vérifier :
  - 10 000 tirs par arme : environ 10 % de ratés pour les armes directes, 25 % pour les explosives (dégâts doublés), 0 % pour les guidées (dégâts minimums), et un rechargement doublé pour les explosives ;
  - les valeurs de `AverageDamage` et `IsDestroyed` avant et après correction ;
  - le comportement de `Remove` avec une autre instance de la même arme (question 6.6).
- La mise en forme a été vérifiée avec `dotnet format`.
- L'IA n'a fait aucun commit : je gère moi-même l'historique git.

## 7. Bilan

L'IA m'a servi d'aide ponctuelle : pour débloquer une notion que je ne connaissais pas encore en C#, pour gagner du temps sur du code répétitif, et pour relire mon travail. Les explications m'ont surtout servi sur les propriétés (auto-propriétés ou calculées, visibilité du `get` et du `set`), sur le contrat entre `Equals` et `GetHashCode`, et sur les interfaces. La relecture a permis de corriger des bugs qui ne provoquaient aucune erreur de compilation et que je n'aurais pas forcément remarqués à l'affichage.
