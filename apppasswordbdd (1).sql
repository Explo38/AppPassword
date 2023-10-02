-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : lun. 02 oct. 2023 à 14:30
-- Version du serveur : 8.0.21
-- Version de PHP : 8.1.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `apppasswordbdd`
--

-- --------------------------------------------------------

--
-- Structure de la table `password_entries`
--

DROP TABLE IF EXISTS `password_entries`;
CREATE TABLE IF NOT EXISTS `password_entries` (
  `id` int NOT NULL AUTO_INCREMENT,
  `site_web` varchar(255) NOT NULL,
  `description` text,
  `user_id` int NOT NULL,
  `url_site_web` varchar(255) NOT NULL DEFAULT '',
  `password_encrypted` blob NOT NULL,
  `encryption_key` blob NOT NULL,
  `encryption_iv` blob NOT NULL,
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Structure de la table `users`
--

DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `first_name` varchar(255) NOT NULL,
  `last_name` varchar(255) NOT NULL,
  `phone` varchar(15) DEFAULT NULL,
  `email` varchar(191) NOT NULL,
  `password_hash` varchar(255) NOT NULL,
  `birth` date DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `email` (`email`)
) ENGINE=MyISAM AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Déchargement des données de la table `users`
--

INSERT INTO `users` (`id`, `first_name`, `last_name`, `phone`, `email`, `password_hash`, `birth`) VALUES
(9, 'a', 'a', NULL, 'a', '$2a$11$WTKuln8UtY98zpsSQbozSOjleMoixNyuv02hV/aBeAJHKUISUNg0.', NULL),
(10, 'b', 'b', NULL, 'b', '$2a$11$anrdjgDpJ/77NInYo.EgoePcqh/xyfR32j.PSDg5pb/UtSNKFazjS', NULL);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
