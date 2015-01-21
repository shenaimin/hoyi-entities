-- --------------------------------------------------------
-- 主机:                           127.0.0.1
-- 服务器版本:                        5.5.5-10.0.14-MariaDB - mariadb.org binary distribution
-- 服务器操作系统:                      Win64
-- HeidiSQL 版本:                  8.3.0.4694
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 导出 sasssdfa 的数据库结构
CREATE DATABASE IF NOT EXISTS `sasssdfa` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `sasssdfa`;


-- 导出  表 sasssdfa.trole 结构
CREATE TABLE IF NOT EXISTS `trole` (
  `roleid` bigint(30) NOT NULL AUTO_INCREMENT COMMENT '角色编号',
  `rolename` varchar(50) NOT NULL COMMENT '角色姓名',
  `roletype` varchar(50) NOT NULL COMMENT '角色类型',
  `notes` varchar(50) NOT NULL COMMENT '备注',
  PRIMARY KEY (`roleid`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COMMENT='角色';

-- 正在导出表  sasssdfa.trole 的数据：~6 rows (大约)
/*!40000 ALTER TABLE `trole` DISABLE KEYS */;
INSERT INTO `trole` (`roleid`, `rolename`, `roletype`, `notes`) VALUES
	(1, 'AA', '', ''),
	(2, 'VV', '', ''),
	(3, 'BB', '', ''),
	(4, 'BD', '', ''),
	(5, 'DDDSX', 'DDDSXA', 'AASDAS'),
	(6, 'DDDSX', 'DDDSXA', 'AASDAS'),
	(7, 'DDDSX', 'DDDSXA', 'AASDAS');
/*!40000 ALTER TABLE `trole` ENABLE KEYS */;


-- 导出  表 sasssdfa.tuser 结构
CREATE TABLE IF NOT EXISTS `tuser` (
  `userid` bigint(30) NOT NULL AUTO_INCREMENT COMMENT '用户编号',
  `username` varchar(50) NOT NULL COMMENT '用户姓名',
  `usertype` varchar(50) NOT NULL COMMENT '用户类型',
  `roleid` bigint(30) NOT NULL COMMENT '角色编号',
  `notes` varchar(50) NOT NULL COMMENT '备注',
  PRIMARY KEY (`userid`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8 COMMENT='用户';

-- 正在导出表  sasssdfa.tuser 的数据：~3 rows (大约)
/*!40000 ALTER TABLE `tuser` DISABLE KEYS */;
INSERT INTO `tuser` (`userid`, `username`, `usertype`, `roleid`, `notes`) VALUES
	(21, 'b00000000', '2', 0, 'bb'),
	(22, 'aa', '1', 0, 'aa'),
	(23, 'b00000000', '2', 0, 'bb');
/*!40000 ALTER TABLE `tuser` ENABLE KEYS */;


-- 导出  表 sasssdfa.user 结构
CREATE TABLE IF NOT EXISTS `user` (
  `userid` int(10) NOT NULL AUTO_INCREMENT COMMENT '用户编号',
  `username` varchar(30) DEFAULT NULL COMMENT '用户名',
  `pwd` varchar(30) NOT NULL COMMENT '密码',
  `amount` decimal(15,2) NOT NULL COMMENT '金额',
  `notes` varchar(50) NOT NULL COMMENT '备注',
  PRIMARY KEY (`userid`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COMMENT='用户';

-- 正在导出表  sasssdfa.user 的数据：~7 rows (大约)
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`userid`, `username`, `pwd`, `amount`, `notes`) VALUES
	(1, 'SS', 'SS', 0.00, ''),
	(2, 'DD', 'DD', 0.00, ''),
	(4, 'Updated', 'WHAT THE FK?', 0.00, ''),
	(6, '你好啊靓仔', '7777', 0.00, ''),
	(7, 'SG', 'GG', 0.00, '');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
