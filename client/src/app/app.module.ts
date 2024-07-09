import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ChatComponent } from './chat/chat.component';
import { JoinRoomComponent } from './join-room/join-room.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { ReactiveFormsModule } from '@angular/forms';
import { JsonPipe } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    ChatComponent,
    JoinRoomComponent,
    WelcomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [
    JsonPipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
