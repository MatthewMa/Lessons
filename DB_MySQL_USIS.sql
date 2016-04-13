-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.7.11-log


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema usis
--

CREATE DATABASE IF NOT EXISTS usis;
USE usis;

--
-- Definition of table `class`
--

DROP TABLE IF EXISTS `class`;
CREATE TABLE `class` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `description` varchar(200) NOT NULL,
  `school_id` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_class_1` (`school_id`),
  CONSTRAINT `FK_class_1` FOREIGN KEY (`school_id`) REFERENCES `school` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `class`
--

/*!40000 ALTER TABLE `class` DISABLE KEYS */;
/*!40000 ALTER TABLE `class` ENABLE KEYS */;


--
-- Definition of table `images`
--

DROP TABLE IF EXISTS `images`;
CREATE TABLE `images` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `url` varchar(100) NOT NULL,
  `title` varchar(100) DEFAULT NULL,
  `screen_id` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_images_1` (`screen_id`),
  CONSTRAINT `FK_images_1` FOREIGN KEY (`screen_id`) REFERENCES `screens` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `images`
--

/*!40000 ALTER TABLE `images` DISABLE KEYS */;
INSERT INTO `images` (`id`,`url`,`title`,`screen_id`) VALUES 
 (1,'http://mgl.usask.ca/usis/images/971gopher2.jpg',' ',2),
 (2,'http://mgl.usask.ca/usis/images/853Lollipop.jpg','Lollipop with ants',4),
 (3,'http://mgl.usask.ca/usis/images/465Gopher.jpg','Gopher',4),
 (4,'http://mgl.usask.ca/usis/images/115.Clay Photo.jpg','',11),
 (5,'http://mgl.usask.ca/usis/images/607.Screen Shot 2016-02-05 at 12.42.21 PM_thumbMed.jpg',NULL,13),
 (6,'http://mgl.usask.ca/usis/images/803.CD Photo 2.jpg',NULL,14),
 (7,'http://mgl.usask.ca/usis/images/336.Screen Shot 2016-02-07 at 4.23.53 PM.png',NULL,15),
 (8,'http://mgl.usask.ca/usis/images/612.FB_IMG_1454017438172_thumbMed.jpg',NULL,20),
 (9,'http://mgl.usask.ca/usis/images/821.FB_IMG_1454017431524_thumbMed.jpg',NULL,22),
 (10,'http://mgl.usask.ca/usis/images/322.FB_IMG_1454017399324_thumbMed.jpg',NULL,23);
/*!40000 ALTER TABLE `images` ENABLE KEYS */;


--
-- Definition of table `lessons`
--

DROP TABLE IF EXISTS `lessons`;
CREATE TABLE `lessons` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(45) NOT NULL,
  `description` varchar(200) DEFAULT NULL,
  `screenCount` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `lessons`
--

/*!40000 ALTER TABLE `lessons` DISABLE KEYS */;
INSERT INTO `lessons` (`id`,`title`,`description`,`screenCount`) VALUES 
 (1,'Jason Horse, Cree Soccer','Soccer lessons',9),
 (2,' Clay Debray & the Muskrat',NULL,10);
/*!40000 ALTER TABLE `lessons` ENABLE KEYS */;


--
-- Definition of table `questions`
--

DROP TABLE IF EXISTS `questions`;
CREATE TABLE `questions` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(200) DEFAULT NULL,
  `detail` varchar(200) DEFAULT NULL,
  `screen_id` int(10) unsigned NOT NULL,
  `q_order` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_questions_1` (`screen_id`),
  CONSTRAINT `FK_questions_1` FOREIGN KEY (`screen_id`) REFERENCES `screens` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `questions`
--

/*!40000 ALTER TABLE `questions` DISABLE KEYS */;
INSERT INTO `questions` (`id`,`title`,`detail`,`screen_id`,`q_order`) VALUES 
 (1,'64 grams',NULL,2,1),
 (2,'54 grams',NULL,2,2),
 (3,'44 grams',NULL,2,3),
 (4,'46 grams',NULL,2,4),
 (5,'39 grams',NULL,2,5),
 (6,' Thunder Horton\'s',NULL,3,1),
 (7,'Sweetgrass',NULL,3,2),
 (8,'Thunderchild',NULL,3,3),
 (9,'Poundmaker',NULL,3,4),
 (10,'Starbucks',NULL,3,5),
 (11,' gopher holes',NULL,8,1),
 (12,' gopher homes',NULL,8,2),
 (13,' elderly gophers',NULL,8,3),
 (14,' donut holes',NULL,8,4),
 (15,' cow droppings',NULL,8,5),
 (16,'The gophers are helping you, it\'s unfair.',NULL,9,4),
 (17,'You have so many squirrels here throwing nuts at us...we don\'t like it. ',NULL,9,1),
 (18,'You have so many duck nests on the field, we can\'t kick the ball. ',NULL,9,2),
 (19,'The balls are falling down the gopher holes and we can\'t play. ',NULL,9,3),
 (20,'You guys have so many gophers holes here, that\'s why we\'re losing. ',NULL,9,5),
 (21,'eating muskrat',NULL,12,1),
 (22,'muskrat',NULL,12,2),
 (23,'eating',NULL,12,3),
 (24,'a gentleman',NULL,12,4),
 (25,'La Pas, Manitoba',NULL,12,5),
 (26,'Muskrats will bite you',NULL,13,1),
 (27,'He would never eat a muskrat',NULL,13,2),
 (28,'Muskrats are cute animals',NULL,13,3),
 (29,'He prefers to eat moose over muskrat',NULL,13,4),
 (30,'It is one of the healthiest animals you can eat',NULL,13,5),
 (31,'medicine',NULL,15,1),
 (32,'muskrat',NULL,15,2),
 (33,'eats medicine',NULL,15,3),
 (34,'muskeg',NULL,15,4),
 (35,'tea',NULL,15,5),
 (36,'rat root and weegusk',NULL,16,1),
 (37,'rat root and tylenol',NULL,16,2),
 (38,'weegusk and vegetables',NULL,16,3),
 (39,'vitamin C and rat root',NULL,16,4),
 (40,'cat root and weegusk',NULL,16,5),
 (41,'favorite root',NULL,18,1),
 (42,'muskrat',NULL,18,2),
 (43,'rat root',NULL,18,3),
 (44,'weegusk',NULL,18,4),
 (45,'muskrat eats',NULL,18,5),
 (46,'weegusk',NULL,19,1),
 (47,'rat root',NULL,19,2),
 (48,'goes into its body',NULL,19,3),
 (49,'eats',NULL,19,4),
 (50,'favorite',NULL,19,5),
 (51,'muskrat',NULL,21,1),
 (52,'eleven year old',NULL,21,2),
 (53,'eyeballs',NULL,21,3),
 (54,'son',NULL,21,4),
 (55,'whole head',NULL,21,5);
/*!40000 ALTER TABLE `questions` ENABLE KEYS */;


--
-- Definition of table `school`
--

DROP TABLE IF EXISTS `school`;
CREATE TABLE `school` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `description` varchar(200) NOT NULL,
  `division_id` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_school_1` (`division_id`),
  CONSTRAINT `FK_school_1` FOREIGN KEY (`division_id`) REFERENCES `school_division` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `school`
--

/*!40000 ALTER TABLE `school` DISABLE KEYS */;
/*!40000 ALTER TABLE `school` ENABLE KEYS */;


--
-- Definition of table `school_division`
--

DROP TABLE IF EXISTS `school_division`;
CREATE TABLE `school_division` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `description` varchar(200) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `school_division`
--

/*!40000 ALTER TABLE `school_division` DISABLE KEYS */;
/*!40000 ALTER TABLE `school_division` ENABLE KEYS */;


--
-- Definition of table `screens`
--

DROP TABLE IF EXISTS `screens`;
CREATE TABLE `screens` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `text` varchar(400) DEFAULT NULL,
  `type` varchar(45) NOT NULL,
  `video_url` varchar(200) DEFAULT NULL,
  `audio_url` varchar(200) DEFAULT NULL,
  `question` varchar(200) DEFAULT NULL,
  `lesson_Id` int(10) unsigned NOT NULL,
  `s_order` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_screens_1` (`lesson_Id`),
  CONSTRAINT `FK_screens_1` FOREIGN KEY (`lesson_Id`) REFERENCES `lessons` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `screens`
--

/*!40000 ALTER TABLE `screens` DISABLE KEYS */;
INSERT INTO `screens` (`id`,`text`,`type`,`video_url`,`audio_url`,`question`,`lesson_Id`,`s_order`) VALUES 
 (1,'Some people feel that gopher consumption is wrong. However, others feel that eating cows, pigs, and chickens is unsustainable. Entomophagy, or eating insects has been proposed to supply human protein needs.  Could it be that gophers could supply North America\'s protein needs? Read the following two articles and then give your reason on whether gophers should be eaten in Canada or not.','audio_text','','http://mgl.usask.ca/usis/audios/78841737.6597gophers or not.mp3',NULL,1,7),
 (2,NULL,'audio_question_image',NULL,'http://mgl.usask.ca/usis/audios/296gopher math.mp3','The weight of a gopher is approximately 230 grams or 0.5 lbs. If the head of a gopher is 20% of its body weight, how many grams does its head weigh?',1,6),
 (3,NULL,'audio_question',NULL,'http://mgl.usask.ca/usis/audios/70588221.7831J Horse Q1 .mp3','What First Nation does this teacher come from?',1,3),
 (4,'If Canada could no longer rely upon domestic cattle, pig, and chicken production to meet it\'s protein consumption needs, what would you rather eat? Insects or gophers? In your essay, provide at least two reasons for your answer.','audio_edittext',NULL,'http://mgl.usask.ca/usis/audios/79744140.7127JH essay.mp3',NULL,1,8),
 (5,NULL,'video','http://mgl.usask.ca/usis/videos/275.CD Muskrat Intro 2 copy.mp4',NULL,NULL,2,1),
 (6,NULL,'video','http://mgl.usask.ca/usis/videos/604Billy Int 2.mp4',NULL,NULL,1,1),
 (7,NULL,'video','http://mgl.usask.ca/usis/videos/604Billy Int 2.mp4',NULL,NULL,1,2),
 (8,NULL,'audio_question',NULL,'http://mgl.usask.ca/usis/audios/73692776.5594JH Q2.mp3','Question two, what were the visitors tripping over?',1,4),
 (9,NULL,'audio_question',NULL,'http://mgl.usask.ca/usis/audios/80793107.9191JH Q.3.mp3','Question three, what did Jason mean when he said this?',1,5),
 (10,'In 15 seconds, speak about the most unusual food you have ever eaten. Press record when you are ready to speak your answer.','text',NULL,NULL,NULL,1,9),
 (11,NULL,'audio_image','','http://mgl.usask.ca/usis/audios/268.CD MP3 - 1.mp3',NULL,2,2),
 (12,NULL,'audio_question',NULL,'http://mgl.usask.ca/usis/audios/954.CD Muskrat Q1.mp3','Question one. What is the topic? The first thing (noun) Mr. Debray mentions. ',2,3),
 (13,NULL,'audio_question_image',NULL,'http://mgl.usask.ca/usis/audios/722.CD Muskrat Q2.mp3','Question two, what did the gentleman in La Pas say about eating muskrat?',2,4),
 (14,NULL,'audio_image',NULL,'http://mgl.usask.ca/usis/audios/844.CD Muskrat L\'g 1.mp3',NULL,2,5),
 (15,NULL,'audio_question_image',NULL,'http://mgl.usask.ca/usis/audios/65.CD Muskrat Q 3.mp3','Question three. What is the topic of what Mr. Dubray spoke of?',2,6),
 (16,NULL,'audio_question',NULL,'http://mgl.usask.ca/usis/audios/33.CD Muskrat Q4.mp3','Question four. Mr. Debray called the vegetation muskrats eat by two other names. What were they?',2,7),
 (17,'Reading Section\r\n\r\nRead the following passage and write down the topic (first mention - noun), and one thing the topic does. (action-verb)\r\n\r\nNow with the muskrat, the muskrat actually eats that root (Weegusk-rat root). That\'s the favorite root of the muskrat. So, when the muskrat eats that, it goes into its body.','audio_text',NULL,'http://mgl.usask.ca/usis/audios/903.CD Reading 2 - Intro.mp3','',2,8),
 (18,NULL,'audio_question',NULL,'http://mgl.usask.ca/usis/audios/369.CD Muskrat Q5.mp3','Question five. What is the topic (first mention-noun) of the reading passage?',2,9),
 (19,NULL,'audio_question',NULL,'http://mgl.usask.ca/usis/audios/532.CD Muskrat Q6 .mp3','Question six. What is the action (verb) of the topic?',2,10),
 (20,'Read the following passage and write down the topic (first mention - noun) and three things the topic does. (action-verbs)\r\n\r\n\r\nI have an eleven year old son who also eats muskrat and when I introduced it to him, he got to eat the whole head of the muskrat. He ate the eyeballs, he ate the brains, and he ate the cheeks.','audio_text_image',NULL,'http://mgl.usask.ca/usis/audios/903.CD Reading 2 - Intro.mp3',NULL,2,11),
 (21,NULL,'audio_question',NULL,'http://mgl.usask.ca/usis/audios/283.CD Muskrat Q7.mp3','Question seven. What is the topic of this passage?',2,12),
 (22,'Writing Section\r\n\r\nWrite down the three things that the topic in the last reading did.\r\n\r\nAnd, write down whether you would eat what the topic ate. Give one reason why you would or would not eat it.','audio_text_image_edittext',NULL,'http://mgl.usask.ca/usis/audios/224.CD Muskrat Writing.mp3',NULL,2,13),
 (23,'Speaking Question\r\n\r\n﻿﻿Say whether you would eat the cheeks, brain, and eyeballs of a muskrat. Give one reason why or why not. \r\n\r\nYou can speak your answer at your desk. If you speak at the front of the classroom, you qualify for Mr. Rowluck\'s bonus. HINT.....It is yummy!','audio_text_image',NULL,'http://mgl.usask.ca/usis/audios/982.CD Muskrat Sp\'g Q.mp3',NULL,2,14);
/*!40000 ALTER TABLE `screens` ENABLE KEYS */;


--
-- Definition of table `student`
--

DROP TABLE IF EXISTS `student`;
CREATE TABLE `student` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `firstname` varchar(45) NOT NULL,
  `lastname` varchar(45) NOT NULL,
  `id_number` varchar(45) NOT NULL,
  `class_id` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_student_1` (`class_id`),
  CONSTRAINT `FK_student_1` FOREIGN KEY (`class_id`) REFERENCES `class` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `student`
--

/*!40000 ALTER TABLE `student` DISABLE KEYS */;
/*!40000 ALTER TABLE `student` ENABLE KEYS */;


--
-- Definition of table `system_users`
--

DROP TABLE IF EXISTS `system_users`;
CREATE TABLE `system_users` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `system_users`
--

/*!40000 ALTER TABLE `system_users` DISABLE KEYS */;
INSERT INTO `system_users` (`id`,`username`,`password`) VALUES 
 (1,'admin','admin1'),
 (2,'test','test@1');
/*!40000 ALTER TABLE `system_users` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
