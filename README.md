

![Logo](https://i.imgur.com/ncycFeY.png)



# BodybuildingManager

BodybuildingManager est un site web de gestion de programme de musculation et de suivis de poids.
Il permet facilement d'enregistrer votre suivis de poids, ainsi que les différentes séances de votre programme.



## Badges


[![MIT License](https://img.shields.io/apm/l/atomic-design-ui.svg?style=for-the-badge)](https://github.com/tterb/atomic-design-ui/blob/master/LICENSEs)
[![GPLv3 License](https://img.shields.io/github/last-commit/vic1997/BodybuildingManager?style=for-the-badge)](https://github.com/vic1997/BodybuildingManager/)


## Docker

Pour déployer l'image docker de l'application, utilisez :

```
docker run -v /host/database.db:/app/database.db vic1997/bodybuildingmanager
```

Il est important de préciser un volume comportant la base de données pour garder les données à chaque extinction du container. Placez le fichier database.db préalablement téléchargé à un endroit dans la machine hôte et indiquez son emplacement direct lors de la création du volume.
 
L'image est disponible à cette adresse :    [vic1997/bodybuildingmanager](https://hub.docker.com/r/vic1997/bodybuildingmanager)

## Fonctionnalités

- Accessible depuis un PC comme un mobile.
- Suivis de poids avec enregistrement retroactif, calcul des évolutions sur plusieurs périodes.
- Enregistrement de programme avec différentes possibilités.
- Ajout de séances unique à un programme, permettant d'enregistrer une évolution.


## Screenshots

![App Screenshot](https://i.imgur.com/kz60PGn.png)
![App Screenshot](https://i.imgur.com/WmK0Bgg.png)
![App Screenshot](https://i.imgur.com/uEPVmxo.png)

## Documentation

#### Concernant le code

Étant donné que ce projet a été développé uniquement pour moi et que je n'avais pas beaucoup de temps pour le faire, j'aimerais indiquer que ce projet n'a pas été développé sérieusement et que le code est très moche (mélange français/anglais, aucune convention de nommage, nombre magique...).
Aucun refactorisation du code n'est prévu pour ce projet, l'objectif serait plutôt de repartir sur un nouveau projet en Blazor si celui-ci devait évoluer.

#### Concernant la base de données

Le code n'est pas prévu pour générer automatiquement la base de données. Le fichier de la base de données vierge peut-être généré avec EFCore, cependant il est également inclus à la racine du projet. Normalement elle est prête à l'emploi. Cependant, pour une utilisation avec docker, il faut le télécharger, le placer dans la machine hôte et créer un volume pointant vers ce fichier, cela est expliqué dans la partie [Docker](https://github.com/vic1997/BodybuildingManager#docker).

## FAQ

#### Pourquoi ce projet ?

Pour développer et parce que les autres applications déjà faites ne me convenaient pas. Là je peux gérer mes données depuis le PC et le mobile et uniquement les données que je veux.

#### Ce projet va-t-il être maintenu ?

Le projet étant passé public, il est censé être fini et utilisable. Quelques bugs peuvent encore être présent et seront peut-être résolu si j'en ai l'envie. Normalement le projet ne devrait pas évoluer plus que ça, car j'ai plutôt prévu de passer sur une nouvelle technologie (Blazor) si j'avais à nouveau l'envie de travailler sur ce projet.

#### Peut-on participer au projet ?

Bien-sûr, je serais toujours présent pour accepter les pull request qui correspondent à une bonne évolution du projet.
## Technologies

**Serveur/Client:** ASP.NET Core MVC

**Framework:** .NET Core 6


## Développeur

- [@Vickes](https://www.github.com/vic1997)

