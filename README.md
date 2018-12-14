<p align="center">
    <img src="https://i.imgur.com/ccpiezK.png" width="200px">
</p>
<h1 align="center">
    Keysafe
</h1>
<h2>Description</h2>
<p>
Keysafe is a password manager built in C# that is intended to keep passwords secure whilst not requiring an internet connection to make API requests.    
</p>
<h3>Benefits</h3>
<ul>
<li>Does not require an internet connection.</li>
<li>Uses AES to encrypt passwords.</li>
</ul>
<h3>Limitations</h3>
<ul>
<li>Ransomware will destroy all data.</li>
<li>Data is only stored on one device.</li>
</ul>
<p>The passwords stored by Keysafe are only decrypted when the hashes are double clicked and are not stored in memory in their decrypted sensitive state.</p>
<h2>Examples</h2>
<h3>Authorisation</h3>
<p>Authorisation occurs when your master key is defined.</p>
<img src="https://i.imgur.com/qHvVCJF.gif">
<h3>Adding an account</h3>
<p>New accounts can easily be added by using the Add form like so. This form also allows secure passwords to be generated.</p>
<img src="https://i.imgur.com/wEBW8zt.gif">
<h3>Importing a pre-exported data file</h3>
<p>Files already exported by Keysafe can be re-imported at any time, this will restart the application and prompt you for the new password.</p>
<img src="https://i.imgur.com/UIxilug.gif">
<p style="color:red;">Remember, you can double click details to copy them to clipboard!</p>
<h2>Releases</h2>
<ul>
    <li><a href="https://github.com/lntel/Keysafe/releases/tag/v1.0.0">v1.0.0</a></li>
</ul>
