import { Component } from '@angular/core';
import { MenuController } from 'ionic-angular';
import { HomePage } from '../home/home';

@Component({
    templateUrl: 'mainHome.html'
})

export class MainHomePage {
    
    rootPage: any = HomePage;

    constructor(public menuCtrl: MenuController) {

    }

    openPage(page) {
        
    }

    openMenu() {
        this.menuCtrl.open();
    }
}