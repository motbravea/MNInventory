-- MNInventory development schema. Contains structure only, with no business records.
-- Run with a database administrator account in a development MySQL instance.
CREATE DATABASE IF NOT EXISTS mninventory
    CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE mninventory;

CREATE TABLE IF NOT EXISTS source_products (
    id INT NOT NULL AUTO_INCREMENT,
    sku VARCHAR(128) NOT NULL DEFAULT '',
    name VARCHAR(255) NOT NULL,
    qr TEXT NOT NULL,
    system_inventory INT NOT NULL DEFAULT 0,
    is_active TINYINT(1) NOT NULL DEFAULT 1,
    imported_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    KEY ix_source_products_sku (sku)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS scanned_products (
    source_product_id INT NOT NULL,
    counted INT NOT NULL DEFAULT 0,
    last_updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (source_product_id),
    CONSTRAINT fk_scanned_source FOREIGN KEY (source_product_id)
        REFERENCES source_products (id)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS scan_history (
    id BIGINT NOT NULL AUTO_INCREMENT,
    source_product_id INT NOT NULL,
    scanned_code TEXT NOT NULL,
    quantity_added INT NOT NULL,
    action_type VARCHAR(16) NOT NULL,
    machine_name VARCHAR(255) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    KEY ix_scan_history_created_at (created_at),
    KEY ix_scan_history_machine (machine_name),
    KEY ix_scan_history_source (source_product_id),
    CONSTRAINT fk_history_source FOREIGN KEY (source_product_id)
        REFERENCES source_products (id)
) ENGINE=InnoDB;
