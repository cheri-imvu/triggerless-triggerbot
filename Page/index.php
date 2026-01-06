<!DOCTYPE html>
<?php
	$thisVersionShort = '1.0.6';
	$thisVersion = $thisVersionShort.'.2601';
?>
<html lang="en">
  <head>
    <meta charset="utf-8" />
	<meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate"/>
	<meta http-equiv="Pragma" content="no-cache"/>
	<meta http-equiv="Expires" content="0"/>
    <link rel="icon" href="favicon.ico" />
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <meta name="theme-color" content="#000000" />
    <meta name="description" content="Triggerless Triggerbot for IMVU" />
	<meta name="og:title" content="Triggerless Triggerbot" />
    <meta name="og:description" content="App for IMVU Trigger DJs to play songs accurately and create new song products" />
    <meta name="og:url" content="https://triggerless.com/triggerbot/" />
    <meta name="og:site_name" content="Triggerless Triggerbot for IMVU" />
    <meta name="og:image" content="https://www.triggerless.com/triggerbot/triggerbot.png" />
    <link rel="stylesheet" href="triggerbot.css" /> 
    <title>Triggerless - Triggerbot for IMVU</title>
    
  </head>
  <body>
    <div class="top">
    <div class="top-left">  
      <img src="triggerbot.png" style="vertical-align: middle" alt="Triggerbot Logo" />
    </div>
    <div class="top-right">        
        <div>Download v1.0</div>
        <div><a class="top-link" href="triggerbot-setup.<?php echo $thisVersion ?>.zip">🠋</a></div>
    </div>
    <div class="center-div">
      <div class="topic">Introduction</div>
        <p>Triggerbot is a Windows app that can help you send song triggers accurately to IMVU. It's currently a work in progress. If you're
          interested, you can download and install it and try it out for yourself.
        </p>
        <p class="img-popup">
          <a href="app-imvu.png" target="_blank"><img src="app.png" /></a>
        </p>
        <p>Additionally, you can easily create new CHKN files from MP3 files on your local computer. This feature is still in
          development and getting better every week.
        </p>
        <p class="img-popup">
          <a href="audio-splice.png" target="_blank"><img src="audio-splice.png" /></a>
        </p>
        <div class="topic">Download Triggerbot</div>
        <p>Download Triggerbot Setup <?php echo $thisVersionShort ?> (release): <a href="triggerbot-setup.<?php echo $thisVersion ?>.zip">triggerbot-setup.<?php echo $thisVersion ?>.zip</a> (55 MB). 
          Once installed, you'll be prompted to upgrade when an update is available.</p>
		<p>Join the Triggerless Discord server! Click here: <a href="https://discord.gg/<?php include 'invite-code.txt' ?>" target="_blank">https://discord.gg/<?php include 'invite-code.txt' ?></a></p>
        <div class="topic">Videos</div>
        <p>Instructional Videos. View in fullscreen mode at 1080p HD for the most realistic experience.</p>
        <p><iframe style="z-index: 10;" width="750" height="422" src="https://www.youtube.com/embed/3p5k97eRWE4" title="YouTube video player" frameborder="0" 
          allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe></p>
        <p><iframe style="z-index: 10;" width="750" height="422" src="https://www.youtube.com/embed/6IPIWJSf5VM" title="YouTube video player" frameborder="0" 
          allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe></p>
    </div>
    <div class="center-div">
      <div class="topic">What's new in Version 1.0.6.2601 (Hotfix) 2026-01-05</div>
      <ul>
		<li>Fixed App Crash during Product Search</li>
		<li>Smoother Playback</li>
      </ul>
    </div>
    <div class="center-div">
      <div class="topic">What's new in Version 1.0.5.2512 (Hotfix) 2025-12-18</div>
      <ul>
		<li>Turkish language fix</li>
		<li>Triggerbot still works after recovering from an IMVU crash</li>
		<li>FLAC/OGG fixed cut fix</li>
		<li>Empty DRVs match new IMVU categories</li>
		<li>Price hiked 30 credits per product cut (sorry)</li>
      </ul>
    </div>
    <div class="center-div">
      <div class="topic">What's new in Version 1.0.4.2510 (Hotfix) 2025-10-12</div>
      <ul>
		<li>Adapted more to new IMVU product categories</li>
      </ul>
    </div>
    <div class="center-div">
      <div class="topic">What's new in Version 1.0.3.2510 (Hotfix) 2025-10-02</div>
      <ul>
        <li>Fixed European decimal point</li>
		<li>Adapted to new IMVU product categories</li>
      </ul>
    </div>
    <div class="center-div">
      <div class="topic">What's new in Version 1.0.2.2509 (Release) 2025-09-25</div>
      <ul>
        <li>Slick new color scheme</li>
		<li>Discord connectivity fixed</li>
		<li>New Lyric Sheet features</li>
		<li>Faster and better conversion to CHKN</li>
		<li>All audio formats supported (FLAC, AAC, WMA, etc.)</li>
		<li>Hotfix for Installation bug</li>
      </ul>
    </div>
	<script>
		function showMore() {
			var more = document.getElementById("more")
			console.log(more.style.display)
			var showMore = document.getElementById("showMore")
			if (more.style.display === "none") {
				more.style.display = "block"
				showMore.innerText = "Show Less"
			}
			else
			{
				more.style.display = "none"
				showMore.innerText = "Show More"
			}
		}				
	</script>
	<div class="center-div">
		<div class="topic">
			<a href="#" onclick="showMore(); return false;">
				<span id="showMore">Show More</span>
			</a>		
		</div>
	</div>
	<div id="more" style="display: none">
		<div class="center-div">
		  <div class="topic">What's new in Version 0.11.6 (beta) 2025-08-16</div>
		  <ul>
			<li>Lyric Sheets editing is back</li>
			<li>Lyrics MP3 player Start/Resume anywhere</li>
			<li>Stay on Top only applies during Playback</li>
		  </ul>
		</div>
		<div class="center-div">
		  <div class="topic">What's new in Version 0.11.1 (beta) 2025-07-19</div>
		  <ul>
			<li>Smart vs. Fixed Cut options</li>
			<li>FFmpeg updated</li>
			<li>Network bugs fixed</li>
			<li>Discord functions moved online</li>
			<li>Lyric Sheet editing is suspended until next release (Sorry)</li>
		  </ul>
		</div>
		<div class="center-div">
		  <div class="topic">What's new in Version 0.10.1 (beta) 2025-04-18</div>
		  <ul>
			<li>Lyric Sheet support. Nuff said.</li>
		  </ul>
		</div>
		<div class="center-div">
		  <div class="topic">What's new in Version 0.10.0 (beta) 2025-04-13</div>
		  <ul>
			<li>Fixed Rescan All Bug</li>
		  </ul>
		</div>
		<div class="center-div">
		  <div class="topic">What's new in Version 0.9.5 (beta) 2025-04-11</div>
		  <ul>
			<li>Improved initial download, Scan New is no longer required.</li>
			<li>Tech Support Tool</li>
			<li>Discord integration</li>
			<li>New DB structure for Lyric Sheets (coming soon)</li>
		  </ul>
		</div>
		<div class="center-div">
		  <div class="topic">What's new in Version 0.9.0 (beta) 2024-12-06</div>
		  <ul>
			<li>Double-click trigger grid row to start over in the middle (for when you mess up a trigger)</li>
			<li>Deep Scan Tool, for scanning any products in inventory, not just Accessories</li>
			<li>Boss Mode</li>
		  </ul>
		</div>
		<div class="center-div">
		  <div class="topic">What's new in Version 0.8.8 (beta) 2023-10-15</div>
		  <ul>
			<li>First-run Database Bug Fixed</li>
			<li>MoveToDeck is async now (to allow continued song play)</li>
			<li>Playback form has started</li>
		  </ul>
		</div>
		<div class="center-div">
		  <div class="topic">What's new in Version 0.8.6 (beta) 2023</div>
		  <ul>
			<li>Saves Lag Factor and other settings</li>
			<li>Save additional triggers to songs</li>
			<li>Code cleanup</li>
		  </ul>
		</div>
		<div class="center-div">
		  <div class="topic">What's new in Version 0.8.4 (beta) 2023</div>
		  <ul>
			<li>New Tab Page Interface</li>
			<li>SendInput reduces lag by 80%</li>
			<li>Improved Lag UI</li>
			<li>Updated third-party libs to most recent</li>
			<li>Fewer typing bugs - IMVU text is cleared before a trigger is sent</li>
			
		  </ul>
		</div>
		<div class="center-div">
		  <div class="topic">What's new in Version 0.8.0 (beta) 2023</div>
		  <ul>
			<li>Invisible Triggers option</li>
			<li>Icon click brings up Product Page</li>
			<li>Allow Full Rescan</li>
			<li>Remove unwanted search result</li>
			<li>Improved trigger detection</li>
			
		  </ul>
		</div>
		<div class="center-div">
		  <div class="topic">What's new in Version 0.4.0 (beta) 2023</div>
		  <ul>
			<li>Much smaller OGG file sizes, most songs can fit in a single CHKN file, without sacrificing sound quality</li>
			<li>Volume adjustment</li>
			<li>Fixed duplicate index error</li>
			<li>Simplified OGG file options</li>
			<li>Auto-detect when a new update is available, MD5 file integrity checked</li>
			
		  </ul>
		</div>
	</div> <!-- more -->
  </body>
</html>
