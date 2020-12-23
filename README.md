# PassMApp
* A desktop app to help you memorise your passwords.PassMApp uses RNGCryptoServiceProvider to create a new salt and SHA256 algorithm to hash the data.

* Security and privacy are two main factors for creating this application, therefore I will keep improving on those sectors of this application until I can't. If you have some help or suggestion please get in touch! xD.


         **Passwords are hashed and salted and then stored into a database.**
I think this is a decent amount of security for a simple desktop application, but I welcome anyone who has a more secure way of storing the passwords. I think we would all benefit from that.

# Updates
### 2.0.0:
* PassMApp has been migrated to a MVVM WPF application to help me test those waters too. 

* A delete button has been added so you can remove some accounts if you wish to instead of having to delete all data completely.
## Using the application
The application asks you to enter the domain of an account. Then you enter the password for it. And a second time for verification; then it is hashed and salted and stored in the database. 
Example: account = Gmail, password = hello.

## Practice Menu
The application shuffles the stored accounts and asks you to enter the password for it. It verifies and if it was the correct password as the one you recorded, it displays the actual password in plaintext. When you enter the correct password and only then can you see the plaintext password.
## Empty Files
The application has an option to delete all stored records and empty the database. This is the Empty Files Option. Click it to delete all data!

# INFORMATION
1. I am working on adding a feature where you receive email notifications if and only you lose 75% of the Practice lessons to rehearse YOUR OWN PASSWORD! Yeah. For security purposes and the thrill.
1. I'm working on an upgrade to provide the ability to create user accounts on this application. Internet is needed to create account but it will only be in order to sign up a recovery email/password input in case of login problems. After a user account has been created, the user can sign in later without internet until user needs to reset password.


Any suggestions or tips are welcome! xD

