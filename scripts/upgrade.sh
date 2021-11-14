#!/bin/bash
echo "this is on the remote $GITHUB_WORKFLOW $GITHUB_RUN_ID" > file.txt
echo "$REMOTE_USER"
scp archive.tar.gz "$REMOTE_USER"@sovize.com:/var/www
tar -xzvf /var/www/archive.tar.gz