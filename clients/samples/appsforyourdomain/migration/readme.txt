gapps_migration_sample: A simple application that demonstrates how to migrate email messages
			to a Google Apps email account. 

To migrate mail as a Google Apps user, invoke as follows:
     gapps_migration_sample <domain> <username> <password>

For example:
    gapps_migration_sample example.com jdoe mypassword

To migrate mail as a Google Apps administrator, also specify the destination 
mailbox (username) for the mail upload:
	gapps_migration_sample <domain> <admin_user> <admin_pass> <destination_user>

For example:
    gapps_migration_sample example.com admin mypassword jdoe