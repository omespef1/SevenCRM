import { NgModule, ErrorHandler } from '@angular/core';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { SevenCRM } from './app.component';
import { AboutPage } from '../pages/about/about';
import { ContactPage } from '../pages/contact/contact';
import { HomePage } from '../pages/home/home';
import { TabsPage } from '../pages/tabs/tabs';
import { MainHomePage } from '../pages/mainHome/mainHome';


@NgModule({
  declarations: [
    SevenCRM,
    AboutPage,
    ContactPage,
    HomePage,
    TabsPage,
    MainHomePage
  ],
  imports: [
    IonicModule.forRoot(SevenCRM)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    SevenCRM,
    AboutPage,
    ContactPage,
    HomePage,
    TabsPage,
    MainHomePage
  ],
  providers: [{provide: ErrorHandler, useClass: IonicErrorHandler}]
})
export class SevenCMRModule {}
