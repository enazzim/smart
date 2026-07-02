package com.shindong.smartmanager.api;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication(scanBasePackages = "com.shindong.smartmanager")
public class SmartManagerApplication {

    public static void main(String[] args) {
        SpringApplication.run(SmartManagerApplication.class, args);
    }
}
