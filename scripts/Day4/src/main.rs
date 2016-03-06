// taken from https://gist.github.com/gkbrk/2e4835e3a17b3fb6e1e7
extern crate crypto;

use crypto::md5::Md5;
use crypto::digest::Digest;

fn main() {
    let mut hasher = Md5::new();

    let key = "ckczppom".as_bytes();
    for i in 0..std::u64::MAX {
        hasher.input(key);
        hasher.input(i.to_string().as_bytes());

        let mut output = [0; 16]; // An MD5 is 16 bytes
        hasher.result(&mut output);

        let first_six = output[0] as i32 + output[1] as i32 + output[2] as i32;
        if first_five == 0 {
            println!("{}", i);
            break;
        }
        hasher.reset();
    }
}
