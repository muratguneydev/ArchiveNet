import { Injectable } from '@angular/core';


@Injectable()
export class EncryptionService {
	constructor() {
	}

	private cryptString(str: string, cryptSeed: number): string {
		var result: string = "";
		for (var i = 0; i < str.length; i++) {
			var characterCode = str.charCodeAt(i);
			characterCode = characterCode + cryptSeed;
			result += String.fromCharCode(characterCode);
		}
		return result;
	}

	encrypt(s: string): string {
		return this.cryptString(s, 1);
	}

	decrypt(s: string): string {
		return this.cryptString(s, -1);
	}

}
