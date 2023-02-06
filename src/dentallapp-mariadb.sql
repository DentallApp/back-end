CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `migration_id` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `product_version` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `pk___ef_migrations_history` PRIMARY KEY (`migration_id`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    ALTER DATABASE CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `appointment_status` (
        `id` int NOT NULL AUTO_INCREMENT,
        `name` longtext CHARACTER SET utf8mb4 NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        CONSTRAINT `pk_appointment_status` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `gender` (
        `id` int NOT NULL AUTO_INCREMENT,
        `name` longtext CHARACTER SET utf8mb4 NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        CONSTRAINT `pk_gender` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `general_treatment` (
        `id` int NOT NULL AUTO_INCREMENT,
        `name` longtext CHARACTER SET utf8mb4 NULL,
        `description` longtext CHARACTER SET utf8mb4 NULL,
        `image_url` longtext CHARACTER SET utf8mb4 NULL,
        `duration` int NOT NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        `is_deleted` tinyint(1) NOT NULL,
        CONSTRAINT `pk_general_treatment` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `kinship` (
        `id` int NOT NULL AUTO_INCREMENT,
        `name` longtext CHARACTER SET utf8mb4 NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        CONSTRAINT `pk_kinship` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `office` (
        `id` int NOT NULL AUTO_INCREMENT,
        `name` longtext CHARACTER SET utf8mb4 NULL,
        `address` longtext CHARACTER SET utf8mb4 NULL,
        `contact_number` longtext CHARACTER SET utf8mb4 NULL,
        `is_enabled_employee_accounts` tinyint(1) NOT NULL,
        `is_checkbox_ticked` tinyint(1) NOT NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        `is_deleted` tinyint(1) NOT NULL,
        CONSTRAINT `pk_office` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `public_holiday` (
        `id` int NOT NULL AUTO_INCREMENT,
        `description` longtext CHARACTER SET utf8mb4 NULL,
        `day` int NOT NULL,
        `month` int NOT NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        `is_deleted` tinyint(1) NOT NULL,
        CONSTRAINT `pk_public_holiday` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `role` (
        `id` int NOT NULL AUTO_INCREMENT,
        `name` longtext CHARACTER SET utf8mb4 NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        CONSTRAINT `pk_role` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `week_day` (
        `id` int NOT NULL AUTO_INCREMENT,
        `name` longtext CHARACTER SET utf8mb4 NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        CONSTRAINT `pk_week_day` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `person` (
        `id` int NOT NULL AUTO_INCREMENT,
        `document` longtext CHARACTER SET utf8mb4 NULL,
        `names` longtext CHARACTER SET utf8mb4 NULL,
        `last_names` longtext CHARACTER SET utf8mb4 NULL,
        `cell_phone` longtext CHARACTER SET utf8mb4 NULL,
        `email` longtext CHARACTER SET utf8mb4 NULL,
        `date_birth` Date NULL,
        `gender_id` int NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        CONSTRAINT `pk_person` PRIMARY KEY (`id`),
        CONSTRAINT `fk_person_gender_gender_id` FOREIGN KEY (`gender_id`) REFERENCES `gender` (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `specific_treatment` (
        `id` int NOT NULL AUTO_INCREMENT,
        `general_treatment_id` int NOT NULL,
        `name` longtext CHARACTER SET utf8mb4 NULL,
        `price` double NOT NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        CONSTRAINT `pk_specific_treatment` PRIMARY KEY (`id`),
        CONSTRAINT `fk_specific_treatment_general_treatment_general_treatment_id` FOREIGN KEY (`general_treatment_id`) REFERENCES `general_treatment` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `holiday_office` (
        `id` int NOT NULL AUTO_INCREMENT,
        `public_holiday_id` int NOT NULL,
        `office_id` int NOT NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        CONSTRAINT `pk_holiday_office` PRIMARY KEY (`id`),
        CONSTRAINT `fk_holiday_office_office_office_id` FOREIGN KEY (`office_id`) REFERENCES `office` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_holiday_office_public_holiday_public_holiday_id` FOREIGN KEY (`public_holiday_id`) REFERENCES `public_holiday` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `office_schedule` (
        `id` int NOT NULL AUTO_INCREMENT,
        `week_day_id` int NOT NULL,
        `office_id` int NOT NULL,
        `start_hour` time(6) NOT NULL,
        `end_hour` time(6) NOT NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        `is_deleted` tinyint(1) NOT NULL,
        CONSTRAINT `pk_office_schedule` PRIMARY KEY (`id`),
        CONSTRAINT `fk_office_schedule_office_office_id` FOREIGN KEY (`office_id`) REFERENCES `office` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_office_schedule_week_day_week_day_id` FOREIGN KEY (`week_day_id`) REFERENCES `week_day` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `user` (
        `id` int NOT NULL AUTO_INCREMENT,
        `username` longtext CHARACTER SET utf8mb4 NULL,
        `password` longtext CHARACTER SET utf8mb4 NULL,
        `refresh_token` longtext CHARACTER SET utf8mb4 NULL,
        `refresh_token_expiry` datetime(6) NULL,
        `person_id` int NOT NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        CONSTRAINT `pk_user` PRIMARY KEY (`id`),
        CONSTRAINT `fk_user_person_person_id` FOREIGN KEY (`person_id`) REFERENCES `person` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `dependent` (
        `id` int NOT NULL AUTO_INCREMENT,
        `user_id` int NOT NULL,
        `person_id` int NOT NULL,
        `kinship_id` int NOT NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        `is_deleted` tinyint(1) NOT NULL,
        CONSTRAINT `pk_dependent` PRIMARY KEY (`id`),
        CONSTRAINT `fk_dependent_kinship_kinship_id` FOREIGN KEY (`kinship_id`) REFERENCES `kinship` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_dependent_person_person_id` FOREIGN KEY (`person_id`) REFERENCES `person` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_dependent_user_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `employee` (
        `id` int NOT NULL AUTO_INCREMENT,
        `user_id` int NOT NULL,
        `person_id` int NOT NULL,
        `office_id` int NOT NULL,
        `pregrade_university` longtext CHARACTER SET utf8mb4 NULL,
        `postgrade_university` longtext CHARACTER SET utf8mb4 NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        `is_deleted` tinyint(1) NOT NULL,
        CONSTRAINT `pk_employee` PRIMARY KEY (`id`),
        CONSTRAINT `fk_employee_office_office_id` FOREIGN KEY (`office_id`) REFERENCES `office` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_employee_person_person_id` FOREIGN KEY (`person_id`) REFERENCES `person` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_employee_user_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `user_role` (
        `id` int NOT NULL AUTO_INCREMENT,
        `user_id` int NOT NULL,
        `role_id` int NOT NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        CONSTRAINT `pk_user_role` PRIMARY KEY (`id`),
        CONSTRAINT `fk_user_role_role_role_id` FOREIGN KEY (`role_id`) REFERENCES `role` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_user_role_user_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `appointment` (
        `id` int NOT NULL AUTO_INCREMENT,
        `user_id` int NOT NULL,
        `person_id` int NOT NULL,
        `dentist_id` int NOT NULL,
        `appointment_status_id` int NOT NULL,
        `general_treatment_id` int NOT NULL,
        `office_id` int NOT NULL,
        `date` Date NOT NULL,
        `start_hour` time(6) NOT NULL,
        `end_hour` time(6) NOT NULL,
        `is_cancelled_by_employee` tinyint(1) NOT NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        CONSTRAINT `pk_appointment` PRIMARY KEY (`id`),
        CONSTRAINT `fk_appointment_appointment_status_appointment_status_id` FOREIGN KEY (`appointment_status_id`) REFERENCES `appointment_status` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_appointment_employee_dentist_id` FOREIGN KEY (`dentist_id`) REFERENCES `employee` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_appointment_general_treatment_general_treatment_id` FOREIGN KEY (`general_treatment_id`) REFERENCES `general_treatment` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_appointment_office_office_id` FOREIGN KEY (`office_id`) REFERENCES `office` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_appointment_person_person_id` FOREIGN KEY (`person_id`) REFERENCES `person` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_appointment_user_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `employee_schedule` (
        `id` int NOT NULL AUTO_INCREMENT,
        `employee_id` int NOT NULL,
        `week_day_id` int NOT NULL,
        `morning_start_hour` time(6) NULL,
        `morning_end_hour` time(6) NULL,
        `afternoon_start_hour` time(6) NULL,
        `afternoon_end_hour` time(6) NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        `is_deleted` tinyint(1) NOT NULL,
        CONSTRAINT `pk_employee_schedule` PRIMARY KEY (`id`),
        CONSTRAINT `fk_employee_schedule_employee_employee_id` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_employee_schedule_week_day_week_day_id` FOREIGN KEY (`week_day_id`) REFERENCES `week_day` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `employee_specialty` (
        `id` int NOT NULL AUTO_INCREMENT,
        `employee_id` int NOT NULL,
        `specialty_id` int NOT NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        CONSTRAINT `pk_employee_specialty` PRIMARY KEY (`id`),
        CONSTRAINT `fk_employee_specialty_employee_employee_id` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_employee_specialty_general_treatment_specialty_id` FOREIGN KEY (`specialty_id`) REFERENCES `general_treatment` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE TABLE `favorite_dentist` (
        `id` int NOT NULL AUTO_INCREMENT,
        `user_id` int NOT NULL,
        `dentist_id` int NOT NULL,
        `created_at` datetime(6) NULL,
        `updated_at` datetime(6) NULL,
        CONSTRAINT `pk_favorite_dentist` PRIMARY KEY (`id`),
        CONSTRAINT `fk_favorite_dentist_employee_dentist_id` FOREIGN KEY (`dentist_id`) REFERENCES `employee` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_favorite_dentist_user_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    INSERT INTO `appointment_status` (`id`, `created_at`, `name`, `updated_at`)
    VALUES (1, TIMESTAMP '2023-02-05 19:09:02', 'Agendada', TIMESTAMP '2023-02-05 19:09:02'),
    (2, TIMESTAMP '2023-02-05 19:09:02', 'Asistida', TIMESTAMP '2023-02-05 19:09:02'),
    (3, TIMESTAMP '2023-02-05 19:09:02', 'No Asistida', TIMESTAMP '2023-02-05 19:09:02'),
    (4, TIMESTAMP '2023-02-05 19:09:02', 'Cancelada', TIMESTAMP '2023-02-05 19:09:02');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    INSERT INTO `gender` (`id`, `created_at`, `name`, `updated_at`)
    VALUES (1, TIMESTAMP '2023-02-05 19:09:02', 'Masculino', TIMESTAMP '2023-02-05 19:09:02'),
    (2, TIMESTAMP '2023-02-05 19:09:02', 'Femenino', TIMESTAMP '2023-02-05 19:09:02');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    INSERT INTO `general_treatment` (`id`, `created_at`, `description`, `duration`, `image_url`, `is_deleted`, `name`, `updated_at`)
    VALUES (1, TIMESTAMP '2023-02-05 19:09:02', 'Es la ciencia y arte que se encarga de ubicar las piezas dentales.', 40, 'ortodoncia.png', FALSE, 'Ortodoncia/brackets', TIMESTAMP '2023-02-05 19:09:02'),
    (2, TIMESTAMP '2023-02-05 19:09:02', 'Su finalidad es la de restaurar o reparar una unidad dentaria (diente) que presenta una cavidad producida por la caries.', 40, 'calce.png', FALSE, 'Calces/resinas', TIMESTAMP '2023-02-05 19:09:02'),
    (3, TIMESTAMP '2023-02-05 19:09:02', 'Es un procedimiento que tiene como finalidad preservar las piezas dentales dañadas, evitando así su pérdida.', 180, 'endodoncia.png', FALSE, 'Tratamiento de conductos/endodoncia', TIMESTAMP '2023-02-05 19:09:02'),
    (4, TIMESTAMP '2023-02-05 19:09:02', 'Es el proceso de cirugía oral más frecuentemente derivado por las unidades de salud bucodental de Atención Primaria.', 90, 'cirugia_molares.png', FALSE, 'Cirugia de tercero molares', TIMESTAMP '2023-02-05 19:09:02'),
    (5, TIMESTAMP '2023-02-05 19:09:02', 'Son elementos metálicos que se ubican quirúrgicamente en los huesos maxilares, debajo de las encías.', 180, 'implantes.png', FALSE, 'Implantes dentales', TIMESTAMP '2023-02-05 19:09:02'),
    (6, TIMESTAMP '2023-02-05 19:09:02', 'Es el proceso por el cual se llevan a cabo determinados procesos hasta conseguir el resultado que busca el paciente en lo que a resultados estéticos se refiere.', 40, 'diseno_sonrisa.png', FALSE, 'Diseño de sonrisa', TIMESTAMP '2023-02-05 19:09:02'),
    (7, TIMESTAMP '2023-02-05 19:09:02', 'Es un tratamiento que se aplica a los dientes que han cambiado de color, siendo uno de los tratamiento estéticos más conservadores.', 40, 'blanqueamiento.png', FALSE, 'Blanqueamiento', TIMESTAMP '2023-02-05 19:09:02'),
    (8, TIMESTAMP '2023-02-05 19:09:02', 'Son aquellas en las que sustituimos uno o varios dientes perdidos y se fijan atornilladas o cementadas sobre el implante.', 40, 'protesis_fijas.png', FALSE, 'Prótesis fijas/removibles', TIMESTAMP '2023-02-05 19:09:02'),
    (9, TIMESTAMP '2023-02-05 19:09:02', 'Es una limpieza con técnicas y herramientas que nos permiten eliminar el sarro, como el detartraje, y la placa bacteriana en todas las zonas de la boca.', 40, 'profilaxis.png', FALSE, 'Profilaxis/fluorización', TIMESTAMP '2023-02-05 19:09:02'),
    (10, TIMESTAMP '2023-02-05 19:09:02', 'Es la especialidad de la odontología que trata las enfermedades de las encías y del hueso que sostiene los dientes.', 40, 'periodoncia.png', FALSE, 'Periodoncia', TIMESTAMP '2023-02-05 19:09:02'),
    (11, TIMESTAMP '2023-02-05 19:09:02', 'Es una rama de la Odontología que atiende y trata las distintas enfermedades bucodentales desde la infancia más temprana hasta finalizar el crecimiento.', 40, 'odontopediatria.png', FALSE, 'Odontopediatria', TIMESTAMP '2023-02-05 19:09:02');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    INSERT INTO `kinship` (`id`, `created_at`, `name`, `updated_at`)
    VALUES (1, TIMESTAMP '2023-02-05 19:09:02', 'Esposo/a', TIMESTAMP '2023-02-05 19:09:02'),
    (2, TIMESTAMP '2023-02-05 19:09:02', 'Hijo/a', TIMESTAMP '2023-02-05 19:09:02'),
    (3, TIMESTAMP '2023-02-05 19:09:02', 'Otros', TIMESTAMP '2023-02-05 19:09:02');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    INSERT INTO `office` (`id`, `address`, `contact_number`, `created_at`, `is_checkbox_ticked`, `is_deleted`, `is_enabled_employee_accounts`, `name`, `updated_at`)
    VALUES (1, 'Mapasingue oeste Av4ta entre calle 4ta, y, Guayaquil 090101', NULL, TIMESTAMP '2023-02-05 19:09:02', TRUE, FALSE, TRUE, 'Mapasingue', TIMESTAMP '2023-02-05 19:09:02'),
    (2, 'Recinto el Piedrero frente al centro de salud', NULL, TIMESTAMP '2023-02-05 19:09:02', TRUE, FALSE, TRUE, 'El Triunfo', TIMESTAMP '2023-02-05 19:09:02'),
    (3, 'Vía principal Naranjito - Bucay, al lado de Ferreteria López', NULL, TIMESTAMP '2023-02-05 19:09:02', TRUE, FALSE, TRUE, 'Naranjito', TIMESTAMP '2023-02-05 19:09:02');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    INSERT INTO `role` (`id`, `created_at`, `name`, `updated_at`)
    VALUES (1, TIMESTAMP '2023-02-05 19:09:02', 'Sin Verificar', TIMESTAMP '2023-02-05 19:09:02'),
    (2, TIMESTAMP '2023-02-05 19:09:02', 'Usuario basico', TIMESTAMP '2023-02-05 19:09:02'),
    (3, TIMESTAMP '2023-02-05 19:09:02', 'Secretaria', TIMESTAMP '2023-02-05 19:09:02'),
    (4, TIMESTAMP '2023-02-05 19:09:02', 'Odontologo', TIMESTAMP '2023-02-05 19:09:02'),
    (5, TIMESTAMP '2023-02-05 19:09:02', 'Administrador', TIMESTAMP '2023-02-05 19:09:02'),
    (6, TIMESTAMP '2023-02-05 19:09:02', 'Superadministrador', TIMESTAMP '2023-02-05 19:09:02');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    INSERT INTO `week_day` (`id`, `created_at`, `name`, `updated_at`)
    VALUES (1, TIMESTAMP '2023-02-05 19:09:02', 'Lunes', TIMESTAMP '2023-02-05 19:09:02'),
    (2, TIMESTAMP '2023-02-05 19:09:02', 'Martes', TIMESTAMP '2023-02-05 19:09:02'),
    (3, TIMESTAMP '2023-02-05 19:09:02', 'Miércoles', TIMESTAMP '2023-02-05 19:09:02'),
    (4, TIMESTAMP '2023-02-05 19:09:02', 'Jueves', TIMESTAMP '2023-02-05 19:09:02'),
    (5, TIMESTAMP '2023-02-05 19:09:02', 'Viernes', TIMESTAMP '2023-02-05 19:09:02'),
    (6, TIMESTAMP '2023-02-05 19:09:02', 'Sábado', TIMESTAMP '2023-02-05 19:09:02');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    INSERT INTO `office_schedule` (`id`, `created_at`, `end_hour`, `is_deleted`, `office_id`, `start_hour`, `updated_at`, `week_day_id`)
    VALUES (1, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 1, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 1),
    (2, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 1, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 2),
    (3, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 1, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 3),
    (4, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 1, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 4),
    (5, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 1, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 5),
    (6, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 2, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 1),
    (7, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 2, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 2),
    (8, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 2, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 3),
    (9, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 2, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 4),
    (10, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 2, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 5),
    (11, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 3, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 1),
    (12, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 3, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 2),
    (13, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 3, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 3),
    (14, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 3, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 4),
    (15, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 3, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 5),
    (16, TIMESTAMP '2023-02-05 19:09:02', TIME '18:00:00', FALSE, 3, TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 6);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    INSERT INTO `person` (`id`, `cell_phone`, `created_at`, `date_birth`, `document`, `email`, `gender_id`, `last_names`, `names`, `updated_at`)
    VALUES (1, '0998994332', TIMESTAMP '2023-02-05 19:09:02', DATE '1999-08-27', '0923611701', 'basic_user@hotmail.com', 1, 'Placencio Pinto', 'Roberto Emilio', TIMESTAMP '2023-02-05 19:09:02'),
    (2, '0998994333', TIMESTAMP '2023-02-05 19:09:02', DATE '1999-07-25', '0923611702', 'secretary@hotmail.com', 2, 'Rodríguez Valencia', 'María Consuelo', TIMESTAMP '2023-02-05 19:09:02'),
    (3, '0998994334', TIMESTAMP '2023-02-05 19:09:02', DATE '1999-07-21', '0923611703', 'dentist@hotmail.com', 1, 'Rodríguez Rivera', 'Guillermo Oswaldo', TIMESTAMP '2023-02-05 19:09:02'),
    (4, '0998994335', TIMESTAMP '2023-02-05 19:09:02', DATE '1999-09-15', '0923611704', 'admin@hotmail.com', 1, 'Figueroa Lopéz', 'Jean Carlos', TIMESTAMP '2023-02-05 19:09:02'),
    (5, '0998994336', TIMESTAMP '2023-02-05 19:09:02', DATE '1999-08-27', '0923611705', 'superadmin@hotmail.com', 1, 'Román Amariles', 'David Sebastian', TIMESTAMP '2023-02-05 19:09:02'),
    (6, '0998994337', TIMESTAMP '2023-02-05 19:09:02', DATE '1999-01-10', '0923611706', 'mary_01@hotmail.com', 2, 'Amariles Valencia', 'María José', TIMESTAMP '2023-02-05 19:09:02'),
    (7, '0998994338', TIMESTAMP '2023-02-05 19:09:02', DATE '1998-02-07', '0923611707', 'torres_02@hotmail.com', 1, 'Torres Rivera', 'Carlos Andrés', TIMESTAMP '2023-02-05 19:09:02');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    INSERT INTO `specific_treatment` (`id`, `created_at`, `general_treatment_id`, `name`, `price`, `updated_at`)
    VALUES (1, TIMESTAMP '2023-02-05 19:09:02', 1, 'Ortodoncia metálica', 80.0, TIMESTAMP '2023-02-05 19:09:02'),
    (2, TIMESTAMP '2023-02-05 19:09:02', 1, 'Ortodoncia autoligado', 150.0, TIMESTAMP '2023-02-05 19:09:02'),
    (3, TIMESTAMP '2023-02-05 19:09:02', 1, 'Ortodoncia estética', 200.0, TIMESTAMP '2023-02-05 19:09:02'),
    (4, TIMESTAMP '2023-02-05 19:09:02', 1, 'Controles ortodoncia', 20.0, TIMESTAMP '2023-02-05 19:09:02'),
    (5, TIMESTAMP '2023-02-05 19:09:02', 1, 'Reposición de Brackets', 3.0, TIMESTAMP '2023-02-05 19:09:02'),
    (6, TIMESTAMP '2023-02-05 19:09:02', 1, 'Reposición de tubos', 5.0, TIMESTAMP '2023-02-05 19:09:02'),
    (7, TIMESTAMP '2023-02-05 19:09:02', 1, 'Microtornillos', 100.0, TIMESTAMP '2023-02-05 19:09:02'),
    (8, TIMESTAMP '2023-02-05 19:09:02', 1, 'Ligas interdentales', 2.0, TIMESTAMP '2023-02-05 19:09:02'),
    (9, TIMESTAMP '2023-02-05 19:09:02', 2, 'Caries compuestas y complejas', 15.0, TIMESTAMP '2023-02-05 19:09:02'),
    (10, TIMESTAMP '2023-02-05 19:09:02', 2, 'Sellantes', 10.0, TIMESTAMP '2023-02-05 19:09:02'),
    (11, TIMESTAMP '2023-02-05 19:09:02', 3, 'Endodoncia central a canino Bio', 90.0, TIMESTAMP '2023-02-05 19:09:02'),
    (12, TIMESTAMP '2023-02-05 19:09:02', 3, 'Endodoncia central a canino Necro', 90.0, TIMESTAMP '2023-02-05 19:09:02'),
    (13, TIMESTAMP '2023-02-05 19:09:02', 3, 'Endodoncia central a canino Retratamiento', 100.0, TIMESTAMP '2023-02-05 19:09:02'),
    (14, TIMESTAMP '2023-02-05 19:09:02', 3, 'Endodoncia de premolares Bio', 120.0, TIMESTAMP '2023-02-05 19:09:02'),
    (15, TIMESTAMP '2023-02-05 19:09:02', 3, 'Endodoncia de premolares Necro', 150.0, TIMESTAMP '2023-02-05 19:09:02'),
    (16, TIMESTAMP '2023-02-05 19:09:02', 3, 'Endodoncia de premolares Retratamiento', 160.0, TIMESTAMP '2023-02-05 19:09:02'),
    (17, TIMESTAMP '2023-02-05 19:09:02', 3, 'Endodoncia molar Bio', 180.0, TIMESTAMP '2023-02-05 19:09:02'),
    (18, TIMESTAMP '2023-02-05 19:09:02', 3, 'Endodoncia premolar Necro', 200.0, TIMESTAMP '2023-02-05 19:09:02'),
    (19, TIMESTAMP '2023-02-05 19:09:02', 3, 'Endodoncia premolar Retratamiento', 200.0, TIMESTAMP '2023-02-05 19:09:02'),
    (20, TIMESTAMP '2023-02-05 19:09:02', 4, 'Exodoncias simples de central a canino', 10.0, TIMESTAMP '2023-02-05 19:09:02'),
    (21, TIMESTAMP '2023-02-05 19:09:02', 4, 'Exodoncias en niños', 10.0, TIMESTAMP '2023-02-05 19:09:02'),
    (22, TIMESTAMP '2023-02-05 19:09:02', 4, 'Exodoncias complejas', 15.0, TIMESTAMP '2023-02-05 19:09:02'),
    (23, TIMESTAMP '2023-02-05 19:09:02', 4, 'Exodoncia 3er molar superior erupcionado', 20.0, TIMESTAMP '2023-02-05 19:09:02'),
    (24, TIMESTAMP '2023-02-05 19:09:02', 4, 'Exodoncia 3er molar superior retenido', 30.0, TIMESTAMP '2023-02-05 19:09:02'),
    (25, TIMESTAMP '2023-02-05 19:09:02', 4, 'Exodoncia 3er molar inferior erupcionado', 30.0, TIMESTAMP '2023-02-05 19:09:02'),
    (26, TIMESTAMP '2023-02-05 19:09:02', 4, 'Exodoncia 3er molar inferior retenido', 50.0, TIMESTAMP '2023-02-05 19:09:02'),
    (27, TIMESTAMP '2023-02-05 19:09:02', 5, 'Implantes', 800.0, TIMESTAMP '2023-02-05 19:09:02'),
    (28, TIMESTAMP '2023-02-05 19:09:02', 6, 'Carillas resinas', 20.0, TIMESTAMP '2023-02-05 19:09:02'),
    (29, TIMESTAMP '2023-02-05 19:09:02', 6, 'Carillas ceromero', 80.0, TIMESTAMP '2023-02-05 19:09:02'),
    (30, TIMESTAMP '2023-02-05 19:09:02', 6, 'Lentes de contacto Emax', 180.0, TIMESTAMP '2023-02-05 19:09:02'),
    (31, TIMESTAMP '2023-02-05 19:09:02', 7, 'Blanqueamiento', 60.0, TIMESTAMP '2023-02-05 19:09:02'),
    (32, TIMESTAMP '2023-02-05 19:09:02', 8, 'Postes fibra de vidrio', 30.0, TIMESTAMP '2023-02-05 19:09:02'),
    (33, TIMESTAMP '2023-02-05 19:09:02', 8, 'IKER', 60.0, TIMESTAMP '2023-02-05 19:09:02'),
    (34, TIMESTAMP '2023-02-05 19:09:02', 8, 'Prótesis parciales acrílicas', 100.0, TIMESTAMP '2023-02-05 19:09:02'),
    (35, TIMESTAMP '2023-02-05 19:09:02', 8, 'Prótesis total acrílica', 120.0, TIMESTAMP '2023-02-05 19:09:02'),
    (36, TIMESTAMP '2023-02-05 19:09:02', 8, 'Meriland metal porcelana', 150.0, TIMESTAMP '2023-02-05 19:09:02'),
    (37, TIMESTAMP '2023-02-05 19:09:02', 8, 'Meriland ceromero', 100.0, TIMESTAMP '2023-02-05 19:09:02'),
    (38, TIMESTAMP '2023-02-05 19:09:02', 8, 'Meriland zirconio', 300.0, TIMESTAMP '2023-02-05 19:09:02'),
    (39, TIMESTAMP '2023-02-05 19:09:02', 8, 'Meriland Emax', 220.0, TIMESTAMP '2023-02-05 19:09:02'),
    (40, TIMESTAMP '2023-02-05 19:09:02', 8, 'Coronas metal porcelana', 80.0, TIMESTAMP '2023-02-05 19:09:02'),
    (41, TIMESTAMP '2023-02-05 19:09:02', 8, 'Corona disilicato', 200.0, TIMESTAMP '2023-02-05 19:09:02'),
    (42, TIMESTAMP '2023-02-05 19:09:02', 8, 'Corona zirconio', 230.0, TIMESTAMP '2023-02-05 19:09:02');
    INSERT INTO `specific_treatment` (`id`, `created_at`, `general_treatment_id`, `name`, `price`, `updated_at`)
    VALUES (43, TIMESTAMP '2023-02-05 19:09:02', 8, 'Corona ceromero', 80.0, TIMESTAMP '2023-02-05 19:09:02'),
    (44, TIMESTAMP '2023-02-05 19:09:02', 8, 'Incrustación ceromero', 70.0, TIMESTAMP '2023-02-05 19:09:02'),
    (45, TIMESTAMP '2023-02-05 19:09:02', 8, 'Incrustación zirconio', 150.0, TIMESTAMP '2023-02-05 19:09:02'),
    (46, TIMESTAMP '2023-02-05 19:09:02', 8, 'Incrustación Emax', 120.0, TIMESTAMP '2023-02-05 19:09:02'),
    (47, TIMESTAMP '2023-02-05 19:09:02', 10, 'Gingivectomias y corte de frenillo (por arcada)', 30.0, TIMESTAMP '2023-02-05 19:09:02');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    INSERT INTO `user` (`id`, `created_at`, `password`, `person_id`, `refresh_token`, `refresh_token_expiry`, `updated_at`, `username`)
    VALUES (1, TIMESTAMP '2023-02-05 19:09:02', '$2a$10$60QnEiafBCLfVBMfQkExVeolyBxVHWcSQKTvkxVJj9FUozRpRP/GW', 1, NULL, NULL, TIMESTAMP '2023-02-05 19:09:02', 'basic_user@hotmail.com'),
    (2, TIMESTAMP '2023-02-05 19:09:02', '$2a$10$60QnEiafBCLfVBMfQkExVeolyBxVHWcSQKTvkxVJj9FUozRpRP/GW', 2, NULL, NULL, TIMESTAMP '2023-02-05 19:09:02', 'secretary@hotmail.com'),
    (3, TIMESTAMP '2023-02-05 19:09:02', '$2a$10$60QnEiafBCLfVBMfQkExVeolyBxVHWcSQKTvkxVJj9FUozRpRP/GW', 3, NULL, NULL, TIMESTAMP '2023-02-05 19:09:02', 'dentist@hotmail.com'),
    (4, TIMESTAMP '2023-02-05 19:09:02', '$2a$10$60QnEiafBCLfVBMfQkExVeolyBxVHWcSQKTvkxVJj9FUozRpRP/GW', 4, NULL, NULL, TIMESTAMP '2023-02-05 19:09:02', 'admin@hotmail.com'),
    (5, TIMESTAMP '2023-02-05 19:09:02', '$2a$10$60QnEiafBCLfVBMfQkExVeolyBxVHWcSQKTvkxVJj9FUozRpRP/GW', 5, NULL, NULL, TIMESTAMP '2023-02-05 19:09:02', 'superadmin@hotmail.com'),
    (6, TIMESTAMP '2023-02-05 19:09:02', '$2a$10$60QnEiafBCLfVBMfQkExVeolyBxVHWcSQKTvkxVJj9FUozRpRP/GW', 6, NULL, NULL, TIMESTAMP '2023-02-05 19:09:02', 'mary_01@hotmail.com'),
    (7, TIMESTAMP '2023-02-05 19:09:02', '$2a$10$60QnEiafBCLfVBMfQkExVeolyBxVHWcSQKTvkxVJj9FUozRpRP/GW', 7, NULL, NULL, TIMESTAMP '2023-02-05 19:09:02', 'torres_02@hotmail.com');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    INSERT INTO `employee` (`id`, `created_at`, `is_deleted`, `office_id`, `person_id`, `postgrade_university`, `pregrade_university`, `updated_at`, `user_id`)
    VALUES (1, TIMESTAMP '2023-02-05 19:09:02', FALSE, 1, 2, 'ESPOL', 'UG', TIMESTAMP '2023-02-05 19:09:02', 2),
    (2, TIMESTAMP '2023-02-05 19:09:02', FALSE, 1, 3, 'ESPOL', 'UG', TIMESTAMP '2023-02-05 19:09:02', 3),
    (3, TIMESTAMP '2023-02-05 19:09:02', FALSE, 1, 4, 'ESPOL', 'UG', TIMESTAMP '2023-02-05 19:09:02', 4),
    (4, TIMESTAMP '2023-02-05 19:09:02', FALSE, 1, 5, 'ESPOL', 'UG', TIMESTAMP '2023-02-05 19:09:02', 5),
    (5, TIMESTAMP '2023-02-05 19:09:02', FALSE, 2, 6, 'ESPOL', 'UG', TIMESTAMP '2023-02-05 19:09:02', 6),
    (6, TIMESTAMP '2023-02-05 19:09:02', FALSE, 3, 7, 'ESPOL', 'UG', TIMESTAMP '2023-02-05 19:09:02', 7);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    INSERT INTO `user_role` (`id`, `created_at`, `role_id`, `updated_at`, `user_id`)
    VALUES (1, TIMESTAMP '2023-02-05 19:09:02', 2, TIMESTAMP '2023-02-05 19:09:02', 1),
    (2, TIMESTAMP '2023-02-05 19:09:02', 3, TIMESTAMP '2023-02-05 19:09:02', 2),
    (3, TIMESTAMP '2023-02-05 19:09:02', 4, TIMESTAMP '2023-02-05 19:09:02', 3),
    (4, TIMESTAMP '2023-02-05 19:09:02', 5, TIMESTAMP '2023-02-05 19:09:02', 4),
    (5, TIMESTAMP '2023-02-05 19:09:02', 6, TIMESTAMP '2023-02-05 19:09:02', 5),
    (6, TIMESTAMP '2023-02-05 19:09:02', 4, TIMESTAMP '2023-02-05 19:09:02', 6),
    (7, TIMESTAMP '2023-02-05 19:09:02', 4, TIMESTAMP '2023-02-05 19:09:02', 7);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    INSERT INTO `employee_schedule` (`id`, `afternoon_end_hour`, `afternoon_start_hour`, `created_at`, `employee_id`, `is_deleted`, `morning_end_hour`, `morning_start_hour`, `updated_at`, `week_day_id`)
    VALUES (1, TIME '18:00:00', TIME '13:00:00', TIMESTAMP '2023-02-05 19:09:02', 2, FALSE, TIME '12:00:00', TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 1),
    (2, TIME '18:00:00', TIME '13:00:00', TIMESTAMP '2023-02-05 19:09:02', 2, FALSE, TIME '12:00:00', TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 2),
    (3, TIME '18:00:00', TIME '13:00:00', TIMESTAMP '2023-02-05 19:09:02', 2, FALSE, TIME '12:00:00', TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 3),
    (4, TIME '18:00:00', TIME '13:00:00', TIMESTAMP '2023-02-05 19:09:02', 2, FALSE, TIME '12:00:00', TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 4),
    (5, TIME '18:00:00', TIME '13:00:00', TIMESTAMP '2023-02-05 19:09:02', 2, FALSE, TIME '12:00:00', TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 5),
    (6, TIME '18:00:00', TIME '13:00:00', TIMESTAMP '2023-02-05 19:09:02', 5, FALSE, TIME '12:00:00', TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 1),
    (7, TIME '18:00:00', TIME '13:00:00', TIMESTAMP '2023-02-05 19:09:02', 5, FALSE, TIME '12:00:00', TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 2),
    (8, TIME '18:00:00', TIME '13:00:00', TIMESTAMP '2023-02-05 19:09:02', 5, FALSE, TIME '12:00:00', TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 3),
    (9, TIME '18:00:00', TIME '13:00:00', TIMESTAMP '2023-02-05 19:09:02', 5, FALSE, TIME '12:00:00', TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 4),
    (10, TIME '18:00:00', TIME '13:00:00', TIMESTAMP '2023-02-05 19:09:02', 5, FALSE, TIME '12:00:00', TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 5),
    (11, TIME '18:00:00', TIME '13:00:00', TIMESTAMP '2023-02-05 19:09:02', 6, FALSE, TIME '12:00:00', TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 1),
    (12, TIME '18:00:00', TIME '13:00:00', TIMESTAMP '2023-02-05 19:09:02', 6, FALSE, TIME '12:00:00', TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 2),
    (13, TIME '18:00:00', TIME '13:00:00', TIMESTAMP '2023-02-05 19:09:02', 6, FALSE, TIME '12:00:00', TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 3),
    (14, TIME '18:00:00', TIME '13:00:00', TIMESTAMP '2023-02-05 19:09:02', 6, FALSE, TIME '12:00:00', TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 4),
    (15, TIME '18:00:00', TIME '13:00:00', TIMESTAMP '2023-02-05 19:09:02', 6, FALSE, TIME '12:00:00', TIME '07:00:00', TIMESTAMP '2023-02-05 19:09:02', 5);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_appointment_appointment_status_id` ON `appointment` (`appointment_status_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_appointment_dentist_id` ON `appointment` (`dentist_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_appointment_general_treatment_id` ON `appointment` (`general_treatment_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_appointment_office_id` ON `appointment` (`office_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_appointment_person_id` ON `appointment` (`person_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_appointment_user_id` ON `appointment` (`user_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_dependent_kinship_id` ON `dependent` (`kinship_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE UNIQUE INDEX `ix_dependent_person_id` ON `dependent` (`person_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_dependent_user_id` ON `dependent` (`user_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_employee_office_id` ON `employee` (`office_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE UNIQUE INDEX `ix_employee_person_id` ON `employee` (`person_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE UNIQUE INDEX `ix_employee_user_id` ON `employee` (`user_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_employee_schedule_employee_id` ON `employee_schedule` (`employee_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_employee_schedule_week_day_id` ON `employee_schedule` (`week_day_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_employee_specialty_employee_id` ON `employee_specialty` (`employee_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_employee_specialty_specialty_id` ON `employee_specialty` (`specialty_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_favorite_dentist_dentist_id` ON `favorite_dentist` (`dentist_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_favorite_dentist_user_id` ON `favorite_dentist` (`user_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_holiday_office_office_id` ON `holiday_office` (`office_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_holiday_office_public_holiday_id` ON `holiday_office` (`public_holiday_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_office_schedule_office_id` ON `office_schedule` (`office_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_office_schedule_week_day_id` ON `office_schedule` (`week_day_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_person_gender_id` ON `person` (`gender_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_specific_treatment_general_treatment_id` ON `specific_treatment` (`general_treatment_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE UNIQUE INDEX `ix_user_person_id` ON `user` (`person_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_user_role_role_id` ON `user_role` (`role_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    CREATE INDEX `ix_user_role_user_id` ON `user_role` (`user_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20230206000903_InitialCreate') THEN

    INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
    VALUES ('20230206000903_InitialCreate', '7.0.2');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

