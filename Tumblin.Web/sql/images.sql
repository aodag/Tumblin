﻿CREATE TABLE `images` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `data` mediumblob,
  `post_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_IMAGES_POSTS_idx` (`post_id`),
  CONSTRAINT `FK_IMAGES_POSTS` FOREIGN KEY (`post_id`) REFERENCES `posts` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
