-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 24-02-2023 a las 01:28:43
-- Versión del servidor: 10.4.24-MariaDB
-- Versión de PHP: 8.0.19

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `heladeria`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clientes`
--

CREATE TABLE `clientes` (
  `idUsuario` int(11) NOT NULL,
  `cedula` varchar(10) DEFAULT NULL,
  `nombres` varchar(30) DEFAULT NULL,
  `apellidos` varchar(30) DEFAULT NULL,
  `telefono` varchar(10) DEFAULT NULL,
  `direccion` varchar(50) DEFAULT NULL,
  `correo` varchar(70) DEFAULT NULL,
  `usuario` varchar(20) DEFAULT NULL,
  `contraseña` varchar(100) DEFAULT NULL,
  `estado` int(11) DEFAULT NULL,
  `imagen` varchar(45) DEFAULT NULL,
  `Perfil_idPerfil` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `clientes`
--

INSERT INTO `clientes` (`idUsuario`, `cedula`, `nombres`, `apellidos`, `telefono`, `direccion`, `correo`, `usuario`, `contraseña`, `estado`, `imagen`, `Perfil_idPerfil`) VALUES
(1, '0602154778', 'cliente', 'cliente', '0987457845', 'Solanda', 'cliente@gmail.com', 'cliente', 'cliente', 1, '', 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleventas`
--

CREATE TABLE `detalleventas` (
  `idDetalleVentas` int(11) NOT NULL,
  `Productos_idProductos` int(11) NOT NULL,
  `Ventas_idVentas` int(11) NOT NULL,
  `cantidad` int(11) NOT NULL,
  `precio_venta` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `detalleventas`
--

INSERT INTO `detalleventas` (`idDetalleVentas`, `Productos_idProductos`, `Ventas_idVentas`, `cantidad`, `precio_venta`) VALUES
(162, 2, 215, 1, 2.75),
(163, 1, 215, 1, 0.75),
(164, 2, 215, 2, 5.5),
(165, 3, 215, 2, 2.5),
(166, 2, 216, 2, 5.5),
(194, 1, 216, 1, 0.75),
(195, 1, 216, 1, 0.75),
(196, 3, 216, 3, 3.75),
(197, 1, 217, 1, 0.75),
(198, 1, 217, 1, 0.75),
(199, 3, 217, 2, 2.5),
(209, 2, 218, 1, 2.75);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `perfil`
--

CREATE TABLE `perfil` (
  `idPerfil` int(11) NOT NULL,
  `nombre` varchar(30) DEFAULT NULL,
  `descripcion` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `perfil`
--

INSERT INTO `perfil` (`idPerfil`, `nombre`, `descripcion`) VALUES
(1, 'Admin', 'Usuario administrador'),
(2, 'User', 'Usuario Normal'),
(3, 'Cliente', '');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productos`
--

CREATE TABLE `productos` (
  `idProductos` int(11) NOT NULL,
  `nombreProducto` varchar(30) DEFAULT NULL,
  `costo` double NOT NULL,
  `adereso` varchar(20) DEFAULT NULL,
  `sabor` varchar(20) DEFAULT NULL,
  `imagen` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `productos`
--

INSERT INTO `productos` (`idProductos`, `nombreProducto`, `costo`, `adereso`, `sabor`, `imagen`) VALUES
(1, 'Helado de Crema ', 0.75, '', 'Chocolate', ''),
(2, 'Helado de Crema', 2.75, '', 'Vainilla', ''),
(3, 'Helado de cono', 1.25, 'queso', 'mora', '');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `idUsuario` int(11) NOT NULL,
  `cedula` varchar(10) NOT NULL,
  `nombres` varchar(30) NOT NULL,
  `apellidos` varchar(30) NOT NULL,
  `telefono` varchar(10) NOT NULL,
  `direccion` varchar(50) NOT NULL,
  `correo` varchar(70) NOT NULL,
  `usuario` varchar(20) NOT NULL,
  `contrasena` varchar(20) NOT NULL,
  `estado` int(11) NOT NULL,
  `imagen` varchar(45) NOT NULL,
  `Perfil_idPerfil` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`idUsuario`, `cedula`, `nombres`, `apellidos`, `telefono`, `direccion`, `correo`, `usuario`, `contrasena`, `estado`, `imagen`, `Perfil_idPerfil`) VALUES
(10, '1751136753', 'david', 'val', '098745784', 'solanda', 'david@gmail.com', 'david01', 'david01', 1, '', 1),
(11, '1784457845', 'angel', 'chi', '0987457845', 'sur', 'angel@gmail.com', 'angel', 'angel', 1, '', 2),
(12, '1784451254', 'andres', 'val', '0987451245', 'norte', 'andres@gmail.com', 'andres03', 'andres03', 1, '', 3),
(19, '1751136753', 'david andres', 'navarro valdiviezo', '0987457845', 'solanda', 'david@gmail.com', 'david', 'david', 1, '', 1),
(20, '0601245778', 'mario', 'castillo', '0987457845', 'sur', 'mario@dgmail.com', 'mario', 'mario', 1, '', 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ventas`
--

CREATE TABLE `ventas` (
  `idVentas` int(11) NOT NULL,
  `numeroVenta` varchar(11) NOT NULL,
  `fecha` varchar(20) NOT NULL,
  `precioTotal` double NOT NULL,
  `Usuario_idUsuario` int(11) NOT NULL,
  `Clientes_idUsuario` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `ventas`
--

INSERT INTO `ventas` (`idVentas`, `numeroVenta`, `fecha`, `precioTotal`, `Usuario_idUsuario`, `Clientes_idUsuario`) VALUES
(213, 'cancelado', '20-02-2023', 45.25, 19, 1),
(214, 'pendiente', '20-02-2023', 4.25, 20, 1),
(215, 'cancelado', '21-02-2023', 11.5, 11, 1),
(216, 'cancelado', '21-02-2023', 10.75, 11, 1),
(217, 'pendiente', '22-02-2023', 4, 11, 1),
(218, 'pendiente', '24-02-2023', 2.75, 19, 1);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `clientes`
--
ALTER TABLE `clientes`
  ADD PRIMARY KEY (`idUsuario`),
  ADD KEY `fk_Clientes_Perfil1_idx` (`Perfil_idPerfil`);

--
-- Indices de la tabla `detalleventas`
--
ALTER TABLE `detalleventas`
  ADD PRIMARY KEY (`idDetalleVentas`),
  ADD KEY `fk_DetalleVentas_Productos1_idx` (`Productos_idProductos`),
  ADD KEY `fk_DetalleVentas_Ventas1_idx` (`Ventas_idVentas`);

--
-- Indices de la tabla `perfil`
--
ALTER TABLE `perfil`
  ADD PRIMARY KEY (`idPerfil`);

--
-- Indices de la tabla `productos`
--
ALTER TABLE `productos`
  ADD PRIMARY KEY (`idProductos`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`idUsuario`),
  ADD KEY `Perfil_idPerfil` (`Perfil_idPerfil`);

--
-- Indices de la tabla `ventas`
--
ALTER TABLE `ventas`
  ADD PRIMARY KEY (`idVentas`),
  ADD KEY `fk_Ventas_Usuario1_idx` (`Usuario_idUsuario`),
  ADD KEY `fk_Ventas_Clientes1_idx` (`Clientes_idUsuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `clientes`
--
ALTER TABLE `clientes`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `detalleventas`
--
ALTER TABLE `detalleventas`
  MODIFY `idDetalleVentas` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=210;

--
-- AUTO_INCREMENT de la tabla `perfil`
--
ALTER TABLE `perfil`
  MODIFY `idPerfil` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `productos`
--
ALTER TABLE `productos`
  MODIFY `idProductos` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT de la tabla `ventas`
--
ALTER TABLE `ventas`
  MODIFY `idVentas` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=219;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `clientes`
--
ALTER TABLE `clientes`
  ADD CONSTRAINT `fk_Clientes_Perfil1` FOREIGN KEY (`Perfil_idPerfil`) REFERENCES `perfil` (`idPerfil`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `detalleventas`
--
ALTER TABLE `detalleventas`
  ADD CONSTRAINT `fk_DetalleVentas_Productos1` FOREIGN KEY (`Productos_idProductos`) REFERENCES `productos` (`idProductos`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_DetalleVentas_Ventas1` FOREIGN KEY (`Ventas_idVentas`) REFERENCES `ventas` (`idVentas`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD CONSTRAINT `usuario_ibfk_1` FOREIGN KEY (`Perfil_idPerfil`) REFERENCES `perfil` (`idPerfil`);

--
-- Filtros para la tabla `ventas`
--
ALTER TABLE `ventas`
  ADD CONSTRAINT `fk_Ventas_Clientes1` FOREIGN KEY (`Clientes_idUsuario`) REFERENCES `clientes` (`idUsuario`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_Ventas_Usuario1` FOREIGN KEY (`Usuario_idUsuario`) REFERENCES `usuario` (`idUsuario`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
