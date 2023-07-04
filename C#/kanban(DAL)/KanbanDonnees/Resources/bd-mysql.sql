DROP DATABASE IF EXISTS `projet`;
CREATE DATABASE `projet`;
USE `projet`;

CREATE TABLE `utilisateur`
(
    `id`          int(10) unsigned NOT NULL AUTO_INCREMENT,
    `nom_complet` varchar(50)      NOT NULL,
    PRIMARY KEY (`id`)
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4;

CREATE TABLE `tableau`
(
    `id`  int(10) unsigned NOT NULL AUTO_INCREMENT,
    `nom` varchar(50)      NOT NULL,
    PRIMARY KEY (`id`)
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4;

CREATE TABLE `liste`
(
    `id`         int(10) unsigned NOT NULL AUTO_INCREMENT,
    `nom`        varchar(50)      NOT NULL,
    `ordre`      int(10) unsigned NOT NULL,
    `tableau_id` int(10) unsigned NOT NULL,
    PRIMARY KEY (`id`),
    FOREIGN KEY (`tableau_id`) REFERENCES `tableau` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4;

CREATE TABLE `carte`
(
    `id`          int(10) unsigned NOT NULL AUTO_INCREMENT,
    `titre`       varchar(30)      NOT NULL,
    `description` text             NOT NULL,
    `echeance`    datetime         NULL,
    `ordre`       int(10) unsigned NOT NULL,
    `liste_id`    int(10) unsigned NOT NULL,
    PRIMARY KEY (`id`),
    FOREIGN KEY (`liste_id`) REFERENCES `liste` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4;

CREATE TABLE `carte_utilisateur`
(
    `utilisateur_id` int(10) unsigned NOT NULL,
    `carte_id`       int(10) unsigned NOT NULL,
    PRIMARY KEY (`utilisateur_id`, `carte_id`),
    FOREIGN KEY (`utilisateur_id`) REFERENCES `utilisateur` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE,
    FOREIGN KEY (`carte_id`) REFERENCES `carte` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4;

INSERT INTO `utilisateur`
VALUES (NULL, 'Quentin Amar');
INSERT INTO `utilisateur`
VALUES (NULL, 'Yves Atrovite');
INSERT INTO `utilisateur`
VALUES (NULL, 'Jean-Marc Desbutes');
INSERT INTO `utilisateur`
VALUES (NULL, 'Tex Agère');
INSERT INTO `utilisateur`
VALUES (NULL, 'Sarah Vigote');

INSERT INTO `tableau`
VALUES (NULL, 'Site web Restaurant du coin inc.');

INSERT INTO `liste`
VALUES (NULL, 'À faire', 0, 1);
INSERT INTO `liste`
VALUES (NULL, 'En cours', 1, 1);
INSERT INTO `liste`
VALUES (NULL, 'À valider', 2, 1);
INSERT INTO `liste`
VALUES (NULL, 'Terminé', 3, 1);

INSERT INTO `carte`
VALUES (NULL, 'Tests', 'Effectuer les tests des cas d''utilisation', NULL, 0, 1);
INSERT INTO `carte`
VALUES (NULL, 'CU1', 'Cas d''utilisation : passer une commande', '2023-02-28', 1, 2);
INSERT INTO `carte`
VALUES (NULL, 'Design', 'Html/css du site web', '2023-02-06', 2, 2);
INSERT INTO `carte`
VALUES (NULL, 'CU2', 'Cas d''utilisation : inscription au site', '2023-02-10', 3, 2);
INSERT INTO `carte`
VALUES (NULL, 'CU3', 'Cas d''utilisation : ajouter/modifier/supprimer des items au panier', '2023-02-15', 4, 2);
INSERT INTO `carte`
VALUES (NULL, 'BD', 'Création de la base de données', '2023-01-31', 5, 3);
INSERT INTO `carte`
VALUES (NULL, 'Devis', 'Faire le devis du projet', '2023-01-01', 6, 4);
INSERT INTO `carte`
VALUES (NULL, 'Prototype', 'Prototype à montrer au client', '2023-01-02', 7, 4);

INSERT INTO `carte_utilisateur`
VALUES (1, 3);
INSERT INTO `carte_utilisateur`
VALUES (2, 4);
INSERT INTO `carte_utilisateur`
VALUES (3, 4);
INSERT INTO `carte_utilisateur`
VALUES (3, 5);
INSERT INTO `carte_utilisateur`
VALUES (1, 6);
INSERT INTO `carte_utilisateur`
VALUES (3, 6);
INSERT INTO `carte_utilisateur`
VALUES (5, 7);
INSERT INTO `carte_utilisateur`
VALUES (2, 8);

INSERT INTO `tableau`
VALUES (NULL, 'Déménagement de bureau');

INSERT INTO `liste`
VALUES (NULL, 'Avant déménagement', 0, 2);
INSERT INTO `liste`
VALUES (NULL, 'Jour déménagement', 1, 2);
INSERT INTO `liste`
VALUES (NULL, 'Après déménagement', 2, 2);
INSERT INTO `liste`
VALUES (NULL, 'Terminé', 3, 2);

INSERT INTO `carte`
VALUES (NULL, 'Boîtes', 'Faire les boîtes', '2023-06-30', 0, 5);
INSERT INTO `carte`
VALUES (NULL, 'Aide', 'Acheter de l''eau et un casse-croûte pour les déménageurs', '2023-07-01', 1, 6);
INSERT INTO `carte`
VALUES (NULL, 'Ménage', 'Faire le ménage de l''ancien bureau après le départ des déménageurs', '2023-07-01', 2, 6);
INSERT INTO `carte`
VALUES (NULL, 'Adresse internet', 'Faire le changement d''adresse auprès de la compagnie d''internet.', '2023-07-07', 3,
        7);
INSERT INTO `carte`
VALUES (NULL, 'Adresse Hydro', 'Faire le changement d''adresse auprès d''Hydro.', '2023-07-07', 4, 7);
INSERT INTO `carte`
VALUES (NULL, 'Réservation', 'Réserver un déménageur.', '2023-06-01', 5, 8);

INSERT INTO `carte_utilisateur`
VALUES (4, 9);
INSERT INTO `carte_utilisateur`
VALUES (5, 9);
INSERT INTO `carte_utilisateur`
VALUES (4, 10);
INSERT INTO `carte_utilisateur`
VALUES (4, 11);
INSERT INTO `carte_utilisateur`
VALUES (4, 12);
INSERT INTO `carte_utilisateur`
VALUES (4, 13);
INSERT INTO `carte_utilisateur`
VALUES (4, 14);

